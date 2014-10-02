// *********************************************** 
// NAME			: FindInputAdapter.cs
// AUTHOR		: C.M. Owczarek
// DATE CREATED	: 26.07.04
// DESCRIPTION	: Responsible for common functionality
// required by Find A input pages.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/FindInputAdapter.cs-arc  $
//
//   Rev 1.6   Feb 16 2010 11:15:32   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.5   Sep 21 2009 14:57:06   mmodi
//Updated for Environmental Benefits Calculator (EBC)
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.4   Oct 13 2008 16:41:26   build
//Automatically merged from branch for stream5014
//
//   Rev 1.3.1.1   Aug 01 2008 16:30:48   mmodi
//Added find a cycle available check
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3.1.0   Jun 20 2008 14:41:08   mmodi
//Updated for cycle journeys
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3   May 01 2008 17:16:46   mmodi
//Updated get transition event methods used by session timeout
//Resolution for 4888: Del 10.1 - Improve Session Timeout Management
//
//   Rev 1.2   Mar 31 2008 12:59:02   mturner
//Drop3 from Dev Factory
//
//  Rev DevFactory Feb 25 2008 14:00:00 mmodi
//  Added properties to turn Find train cost functionality on or off.
//
//  Rev DevFactory Feb 15 2008 08:47:00 apatel
//  Added properties to turn input page functionalities on or off.
//
//   Rev 1.0   Nov 08 2007 13:11:20   mturner
//Initial revision.
//
//   Rev 1.36   Nov 16 2006 16:25:24   tmollart
//Added property to detemine if Rail Cost property is set.
//Resolution for 4220: Rail Search by Price
//
//   Rev 1.35   Nov 14 2006 09:55:08   rbroddle
//Merge for stream4220
//
//   Rev 1.34.1.0   Nov 07 2006 11:28:04   tmollart
//Added/Updated methods for Rail Search By Price.
//Resolution for 4220: Rail Search by Price
//
//   Rev 1.34   Oct 06 2006 14:05:34   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.33.1.0   Aug 23 2006 14:43:44   mmodi
//Added FindCarPark transition event
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.33   May 04 2006 10:57:44   mtillett
//Add logic to test for park and ride when FindAMode is Car
//Resolution for 4072: DN058 Park and Ride Phase 2: Destination field is empty on ambiguity page after entering invalid date in the Amend Tool
//
//   Rev 1.32   Apr 26 2006 12:15:02   RPhilpott
//Manual merge of stream 35
//
//   Rev 1.30.2.0   Mar 29 2006 18:23:28   RPhilpott
//Split properties that switch find-a-fare on and off to allow find-cheaper to be used separately.
//Resolution for 35: DEL 8.1 Workstream - Find Cheaper (Rail)
//
//   Rev 1.30   Feb 23 2006 19:16:08   build
//Automatically merged from branch for stream3129
//
//   Rev 1.29.1.1   Jan 30 2006 12:18:54   mdambrine
//add TDCultureInfo is now in the common namespace
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.29.1.0   Jan 10 2006 15:17:36   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.29   Nov 14 2005 14:41:20   ralonso
//Commented code removed
//
//   Rev 1.28   Nov 09 2005 16:59:24   jgeorge
//Manual merge for stream2818 (Search by Price)
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.27   Nov 03 2005 17:04:00   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.26.1.1   Oct 24 2005 21:21:38   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.26.1.0   Oct 10 2005 13:05:06   rgreenwood
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.26   Apr 20 2005 11:25:36   tmollart
//Modified TransitionEvent method so user taken to correct place if they have cost based results.
//Resolution for 2147: PT - Cost based city to city planner automatically jumps to previously planned find a fare results when selected.
//
//   Rev 1.25   Apr 15 2005 12:47:30   COwczarek
//Changes to allow PT cost based searches to work with extend journey functionality.
//Resolution for 2079: PT Extend journey does not work with PT cost based searches
//
//   Rev 1.24   Mar 24 2005 14:20:28   rgeraghty
//Temporary switch implemented for costbasedsearching
//Resolution for 1972: Front End Switch for Find A Fare/City to City Cost Based Search
//
//   Rev 1.23   Mar 17 2005 14:05:52   rhopkins
//Move journeyParams FindJourneyInputAdapter
//Resolution for 1927: DEV Code Review: FAF date selection
//Resolution for 1932: DEV Code Review: FAF Output Pages
//
//   Rev 1.22   Feb 25 2005 16:43:38   COwczarek
//Add isCostBasedSearch method to test if Find A Mode is a
//cost based search mode.
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.21   Jan 28 2005 18:41:56   ralavi
//updated for car costing
//
//   Rev 1.20   Jan 25 2005 12:14:04   rhopkins
//Refactor ...InputAdapter classes to allow FindInputAdapter to be inherited by FindFareInputAdapter.
//
//   Rev 1.19   Nov 03 2004 12:54:04   passuied
//Changes to enable a new FindAMode TrunkStation similar to Trunk but with differences...
//
//   Rev 1.18   Nov 02 2004 15:54:34   passuied
//Changes in title, error messages and instructions for FindTrunkInput
//
//   Rev 1.17   Oct 15 2004 12:37:04   jgeorge
//Added abstract GetJourneyPlanControlData method and made class abstract.
//Resolution for 1713: Results are incorrect after planning several journeys
//
//   Rev 1.16   Sep 16 2004 17:50:34   jmorrissey
//Updated error message display.
//
//   Rev 1.15   Sep 14 2004 17:06:32   jmorrissey
//Removed 'Then click Next' message from end of 'overlapping naptan' validation error message.
//
//   Rev 1.14   Sep 13 2004 12:17:02   jmorrissey
//IR1527 - added validation for overlapping origin, destination and via locations before doing a journey search
//
//   Rev 1.13   Sep 02 2004 17:01:56   passuied
//Added new constant for AnyTime value to avoid duplication of hardcoded value "Any"
//Resolution for 1465: FindA "Amend journey" does not work with "Any time"
//
//   Rev 1.12   Aug 31 2004 10:34:00   passuied
//Set Ambiguity Mode when clicking Amend DateTime Ok button + Moved Date Ambiguity Setting before DateControl population
//Resolution for 1373: Duplication of code for redirecting to appropriate FindA page
//
//   Rev 1.11   Aug 27 2004 17:02:48   RPhilpott
//Correct interaction between control hierarchy and LocationSearchHelper.
//Resolution for 1329: Both train and coach stops can be selected on a Find A Train or Find A Coach page
//
//   Rev 1.10   Aug 27 2004 10:40:06   passuied
//missing change for IR.
//Resolution for 1416: Find a car does not recognise postcode that is OK in journey planner
//
//   Rev 1.9   Aug 26 2004 16:05:26   passuied
//Adding some resource keys which are common to all FindA pages.
//Resolution for 1444: Find A Car : Missing Back button at top of page
//
//   Rev 1.8   Aug 24 2004 18:05:36   RPhilpott
//Pass StationType to LocationSerachHelper to allow filtering by MajorStations gazetteer.
//Resolution for 1329: Both train and coach stops can be selected on a Find A Train or Find A Coach page
//
//   Rev 1.7   Aug 23 2004 13:11:40   passuied
//Added overloaded version of AmbiguitySearch which specifies if must accept postcodes or not.
//Resolution for 1416: Find a car does not recognise postcode that is OK in journey planner
//
//   Rev 1.6   Aug 19 2004 12:47:52   COwczarek
//Add GetHeaderTransitionEventFromMode method
//Resolution for 1345: Clicking Find A tab should display page for current Find A mode
//
//   Rev 1.5   Aug 18 2004 14:19:22   passuied
//Use of non duplicated code to Get transitionevent from FindAMode mode
//
//   Rev 1.4   Aug 13 2004 16:26:54   esevern
//added initialise find date control method
//
//   Rev 1.3   Aug 04 2004 14:25:44   passuied
//changed IsAtHighesLevel() method to take into account PrivateVia as well
//
//   Rev 1.2   Aug 02 2004 14:49:10   passuied
//added MapSearch public method to Set up location search when map button is clicked next to a location
//
//   Rev 1.1   Jul 29 2004 17:08:18   COwczarek
//Set return required flag in journey parameters
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.0   Jul 29 2004 11:19:10   COwczarek
//Initial revision.
//Resolution for 1202: Implement FindTrainInput page

using System;
using System.Web.UI.WebControls;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.PropertyService.Properties;

using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.LocationService;

using TransportDirect.JourneyPlanning.CJPInterface;

using TransportDirect.Web.Support;


namespace TransportDirect.UserPortal.Web.Adapters
{

	/// <summary>
	/// Responsible for common functionality required by Find A input pages.
	/// </summary>
	public abstract class FindInputAdapter
	{

		public static readonly string BackTextKey = "FindPageOptionsControl.Back.Text";

		public static readonly string AnyTimeValue = "Any";
		
		protected string LocationsUnspecifiedKey = "ValidateAndRun.SelectLocation";


		/// <summary>
		/// Page state for Find A input pages
		/// </summary>
		protected FindPageState pageState;

		//session manager
		protected ITDSessionManager tdSessionManager;


		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="pageState">Page state for Find A input pages</param>
		/// <param name="sessionManager">Current SessionManager</param>
		public FindInputAdapter(FindPageState pageState, ITDSessionManager sessionManager)
		{
			this.pageState = pageState;
			this.tdSessionManager = sessionManager;
		}

		#region Protected Methods

		/// <summary>
		/// Hook method for saving mode specific travel preferences. Called by SaveTravelDetails.
		/// Subclasses must override this method.
		/// </summary>
		/// <param name="travelPreferences"></param>
		protected virtual void saveModeSpecificPreferences(TDProfile travelPreferences) 
		{
		}

		/// <summary>
		/// Hook method for loading mode specific travel preferences. Called by LoadTravelDetails.
		/// Subclasses must override this method.
		/// </summary>
		/// <param name="travelPreferences"></param>
		protected virtual void loadModeSpecificPreferences(TDProfile travelPreferences) 
		{
		}

		#endregion Protected Methods

		#region Public Methods

		/// <summary>
		/// Initialises the AsyncCallState for the input adapter.
		/// Subclasses must override this method.
		/// </summary>
		public abstract void InitialiseAsyncCallState();

		/// <summary>
		/// Initialises journey parameters with default values for Find A journey planning.
		/// Subclasses must override this method.
		/// </summary>
		public abstract void InitJourneyParameters();

		/// <summary>
		/// Initialises the locationsControl with journey parameters
		/// Subclasses must override this method.
		/// </summary>
		/// <param name="locationsControl">The control to initialise</param>
		public abstract void InitLocationsControl(FindToFromLocationsControl locationsControl);

		/// <summary>
		/// Initialises the FindLeaveReturnDatesControl with journey parameters.  
		/// Subclasses must override this method.
		/// </summary>
		/// <param name="locationsControl">The control to initialise</param>
		public abstract void InitDateControl(FindLeaveReturnDatesControl dateControl);

		/// <summary>
		/// Initialises the dateControl with journey parameters
		/// Subclasses must override this method.
		/// </summary>
		/// <param name="dateControl">The control to initialise</param>
		public abstract void UpdateDateControl(FindLeaveReturnDatesControl dateControl);

		/// <summary>
		/// Updates the journey parameters with the values of dateControl
		/// Subclasses must override this method.
		/// </summary>
		/// <param name="dateControl">The control supplying the values</param>
		public abstract void UpdateJourneyDates(FindLeaveReturnDatesControl dateControl);

		/// <summary>
		/// Template method that loads travel details from user preferences.
		/// loadModeSpecificPreferences must be overidden to load mode specific values.
		/// </summary>
		public virtual void LoadTravelDetails()
		{
			if (tdSessionManager.Authenticated && (tdSessionManager.JourneyParameters is TDJourneyParametersMulti)) 
			{
				TDProfile travelPreferences = tdSessionManager.CurrentUser.UserProfile;
				loadModeSpecificPreferences(travelPreferences);
			}
		}

		/// <summary>
		/// Template method that saves travel details to user preferences.
		/// saveModeSpecificPreferences must be overidden to save mode specific values.
		/// </summary>
		public virtual void SaveTravelDetails() 
		{
			if (tdSessionManager.Authenticated) 
			{
				TDProfile travelPreferences = tdSessionManager.CurrentUser.UserProfile;
				saveModeSpecificPreferences(travelPreferences);
				travelPreferences.Update();
			}
		}

		/// <summary>
		/// Returns true if supplied ValidationError object contains errors indicating
		/// erroneous results for any location (to, from, public via or private via).
		/// </summary>
		/// <param name="errors">errors to search</param>
		/// <returns>True if errors indicate erroneous results for any location, false otherwise</returns>
		public virtual bool AreOriginAndDestinationLocationsOverlapping(ValidationError errors)
		{
			
			return (errors.Contains(ValidationErrorID.OriginAndDestinationOverlap));
			
		}

		/// <summary>
		/// Updates supplied label with message to correct errors. Message depends on errors found in supplied
		/// errors object. The visibility of panelErrorMessage is set to true if the label is set with a message
		/// or false if not.
		/// </summary>
		/// <param name="panelErrorMessage">Panel to set visibility of</param>
		/// <param name="labelErrorMessages">Label to update with message</param>
		/// <param name="errors">errors to search</param>
		public abstract void UpdateErrorMessages(Panel panelErrorMessage, Label labelErrorMessages, ValidationError errors);

		#endregion Public Methods


		#region Static methods

		/// <summary>
		/// Static read-only property indicating if find a fare is to be made available
		/// </summary>
		/// <returns>boolean</returns>
		public static bool FindAFareAvailable
		{
			get { return bool.Parse(Properties.Current["FindAFareAvailable"]); }
		}

		/// <summary>
		/// Statis read-only property indicating if find a fare for rail cost searches
		/// is available.
		/// </summary>
		public static bool FindAFareAvailableRailCost
		{
			get { return bool.Parse(Properties.Current["FindAFareAvailable.Rail"]); }
		}

        /// <summary>
        /// Static read-only property indicating if Door to Door Journey
        /// is available.
        /// </summary>
        public static bool DoorToDoorJourneyAvailable
        {
            get 
            {
                string property = Properties.Current["DoorToDoorJourneyAvailable"];
                return property != null ? bool.Parse(property) : true; 
            }
        }

        /// <summary>
        /// Static read-only property indicating if Find a Train search
        /// is available.
        /// </summary>
        public static bool FindATrainAvailable
        {
            get 
            {
                string property = Properties.Current["FindATrainAvailable"];
                return property != null ? bool.Parse(property) : true;
            }
        }

        /// <summary>
        /// Static read-only property indicating if Find a Train search
        /// is available.
        /// </summary>
        public static bool FindATrainCostAvailable
        {
            get
            {
                string property = Properties.Current["FindAFareAvailable.Rail"];
                return property != null ? bool.Parse(property) : true;
            }
        }

        /// <summary>
        /// Static read-only property indicating if Find a Flight search
        /// is available.
        /// </summary>
        public static bool FindAFlightAvailable
        {
            get 
            {
                string property = Properties.Current["FindAFlightAvailable"];
                return property != null ? bool.Parse(property) : true;
            }
        }

        /// <summary>
        /// Static read-only property indicating if Find a Car Route search
        /// is available.
        /// </summary>
        public static bool FindACarAvailable
        {
            get 
            {
                string property = Properties.Current["FindACarAvailable"];
                return property != null ? bool.Parse(property) : true;
            }
        }

        /// <summary>
        /// Static read-only property indicating if Find a Coach search
        /// is available.
        /// </summary>
        public static bool FindACoachAvailable
        {
            get 
            {
                string property = Properties.Current["FindACoachAvailable"];
                return property != null ? bool.Parse(property) : true; 
            }
        }

        /// <summary>
        /// Static read-only property indicating if Find a Cycle search
        /// is available.
        /// </summary>
        public static bool FindACycleAvailable
        {
            get
            {
                string property = Properties.Current["CyclePlanner.FindACycleAvailable"];
                return property != null ? bool.Parse(property) : true;
            }
        }

        /// <summary>
        /// Static read-only property indicating if Find Environmental Benefits Calculator search
        /// is available.
        /// </summary>
        public static bool FindEBCAvailable
        {
            get
            {
                string property = Properties.Current["EnvironmentalBenefitsCalculator.Available"];
                return property != null ? bool.Parse(property) : true;
            }
        }

        /// <summary>
        /// Static read-only property indicating if City to City Journey Compare search
        /// is available.
        /// </summary>
        public static bool CompareCityToCityJourneyAvailable
        {
            get 
            {
                string property = Properties.Current["CompareCityToCityJourneyAvailable"];
                return property != null ? bool.Parse(property) : true; 
            }
        }

        /// <summary>
        /// Static read-only property indicating if Plan A Day Trip search
        /// is available.
        /// </summary>
        public static bool PlanADayTripAvailable
        {
            get 
            {
                string property = Properties.Current["PlanADayTripAvailable"];
                return property != null ? bool.Parse(property) : true;
            }
        }

        /// <summary>
        /// Static read-only property indicating if Plan To Park And Ride search
        /// is available.
        /// </summary>
        public static bool PlanToParkAndRideAvailable
        {
            get {
                string property = Properties.Current["PlanToParkAndRideAvailable"];
                return property != null ? bool.Parse(property) : true; 
            }
        }

        /// <summary>
        /// Static read-only property indicating if Find A Bus search
        /// is available.
        /// </summary>
        public static bool FindABusAvailable
        {
           
            get { 
                string property = Properties.Current["FindABusAvailable"];
                return property != null ? bool.Parse(property) : true; 
            }
        }

        /// <summary>
        /// Static read-only property indicating if Find A Station search
        /// is available.
        /// </summary>
        public static bool FindAStationAvailable
        {
            get {
                string property = Properties.Current["FindAStationAvailable"];
                return property != null ? bool.Parse(property) : true; 
                
            }
        }

        /// <summary>
        /// Static read-only property indicating if HomeImageButton 
        /// is available
        /// </summary>
        public static bool HomeImageButtonAvailable
        {
            get
            {
                string property = Properties.Current["HomeImageButtonAvailable"];
                return property != null ? bool.Parse(property) : true;

            }
        }

        /// <summary>
        /// Static read-only property indicating if PlanAJourneyImageButton 
        /// is available
        /// </summary>
        public static bool PlanAJourneyImageButtonAvailable
        {
            get
            {
                string property = Properties.Current["PlanAJourneyImageButtonAvailable"];
                return property != null ? bool.Parse(property) : true;

            }
        }

        /// <summary>
        /// Static read-only property indicating if FindAPlaceImageButton
        /// is available
        /// </summary>
        public static bool FindAPlaceImageButtonAvailable
        {
            get
            {
                string property = Properties.Current["FindAPlaceImageButtonAvailable"];
                return property != null ? bool.Parse(property) : true;

            }
        }

        /// <summary>
        /// Static read-only property indicating if LiveTravelImageButton
        /// is available
        /// </summary>
        public static bool LiveTravelImageButtonAvailable
        {
            get
            {
                string property = Properties.Current["LiveTravelImageButtonAvailable"];
                return property != null ? bool.Parse(property) : true;

            }
        }

        /// <summary>
        /// Static read-only property indicating if TipsAndToolsImageButton
        /// is available
        /// </summary>
        public static bool TipsAndToolsImageButtonAvailable
        {
            get
            {
                string property = Properties.Current["TipsAndToolsImageButtonAvailable"];
                return property != null ? bool.Parse(property) : true;

            }
        }

        /// <summary>
        /// Static read-only property indicating if LoginRegisterImageButton
        /// is available
        /// </summary>
        public static bool LoginRegisterImageButtonAvailable
        {
            get
            {
                string property = Properties.Current["LoginRegisterImageButtonAvailable"];
                return property != null ? bool.Parse(property) : true;

            }
        }

		/// <summary>
		/// Returns whether "find cheaper" is to be 
		/// made available for the specified mode 
		/// </summary>
		/// <param name="mode">Mode of this journey</param>
		/// <returns>boolean</returns>
		public static bool IsFindCheaperAvailable(ModeType mode)
		{
			string property = Properties.Current["FindCheaperAvailable." + mode.ToString()];
			return (property != null ? bool.Parse(property) : false);
		}

		/// <summary>
		/// Gets the appropriate TransitionEvent from the given FindA mode
		/// Returns TransitionEvent.Empty if supplied mode is None.
        /// If mode is Car, will check Itinerary Manager to see if event returned should be ParkAndRideInput or FindCarInputDefault
		/// </summary>
		/// <param name="mode">mode to get the TransitionEvent from</param>
		/// <returns>Transition event</returns>
		public static TransitionEvent GetTransitionEventFromMode(FindAMode mode)
		{
			switch(mode)
			{
				case FindAMode.Flight:
					return TransitionEvent.FindFlightInputDefault;

				case FindAMode.Coach:
					return TransitionEvent.FindCoachInputDefault;

				case FindAMode.Train:
					return TransitionEvent.FindTrainInputDefault;

				case FindAMode.TrunkStation:
				case FindAMode.Trunk:
					return TransitionEvent.FindTrunkInputDefault;

				case FindAMode.Station:
					return TransitionEvent.FindStationInputDefault;

				case FindAMode.Car:
                    try
                    {
                        TDItineraryManager itineraryManager = TDItineraryManager.Current;
                        if (itineraryManager.FromParkAndRideInput || itineraryManager.JourneyParameters.DestinationLocation.ParkAndRideScheme != null)
                        {
                            return TransitionEvent.ParkAndRideInput;
                        }
                        else
                        {
                            return TransitionEvent.FindCarInputDefault;
                        }
                    }
                    catch
                    {
                        return TransitionEvent.FindCarInputDefault;
                    }

				case FindAMode.Bus:
					return TransitionEvent.FindBusInputDefault;

				case FindAMode.Fare:
				case FindAMode.TrunkCostBased:
					return TransitionEvent.FindFareInputDefault;

				case FindAMode.CarPark:
					return TransitionEvent.FindCarParkInputDefault;

				case FindAMode.RailCost:
					return TransitionEvent.FindTrainCostInputDefault;

                case FindAMode.Cycle:
                    return TransitionEvent.FindCycleInputDefault;

                case FindAMode.EnvironmentalBenefits:
                    return TransitionEvent.FindEBCInputDefault;

                case FindAMode.International:
                    return TransitionEvent.FindInternationalInputDefault;
                    
				default:
					return TransitionEvent.Empty;

			}

		}

        /// <summary>
        /// Gets the appropriate transition event from the given mode suitable for
        /// use by header controls. If the value of the supplied mode is None then
        /// TransitionEvent.FindADefault is returned.
        /// </summary>
        /// <param name="mode">mode to get the transition event from</param>
        /// <returns>The transition event corresponding to the supplied mode</returns>
        public static TransitionEvent GetHeaderTransitionEventFromMode(FindAMode mode)
        {
            if (mode == FindAMode.None) 
            {
                return TransitionEvent.FindADefault;
            } 
            else 
            {
				// Here special case. When comes from the header, 
				// in TrunkStation mode, we want to be redirected to StationInput page
				// so if there are valid results we are taken there!
				if ( mode == FindAMode.TrunkStation)
					return TransitionEvent.FindStationInputDefault;
				else
                return GetTransitionEventFromMode(mode);
            }
        }

        /// <summary>
        /// Gets the appropriate transition event from the given mode suitable.
        /// If mode is ParkAndRide, will return TransitionEvent.ParkAndRideInput.
        /// If mode is None, will return TransitionEvent.Empty.
        /// </summary>
        /// <param name="mode">mode to get the transition event from</param>
        /// <returns>The transition event corresponding to the supplied mode</returns>
        public static TransitionEvent GetTransitionEventFromModeAll(FindAMode mode)
        {
            if (mode == FindAMode.ParkAndRide)
            {
                return TransitionEvent.ParkAndRideInput;
            }
            else
            {
                return GetTransitionEventFromMode(mode);
            }
        }

        /// <summary>
        /// Determines the Find A page to redirect to when the user clicks the Find A tab.
        /// </summary>
        /// <param name="sessionManager">session manager from which to obtain session data</param>
        /// <returns>Find A page to redirect to when the user clicks the Find A tab</returns>
        public static TransitionEvent GetFindATransitionEvent(ITDSessionManager sessionManager) 
        {
			//If the user has cost based results we need to determine which find a (fare or trunk)
			//they used to plan the journey and redirect them to the appropriate results page.
            if (sessionManager.HasCostBasedFaresResults || sessionManager.HasCostBasedJourneyResults) 
            {
				if (sessionManager.CostBasedFindAMode == FindAMode.TrunkCostBased)
					return TransitionEvent.FindTrunkInputDefault;
				else
					return TransitionEvent.FindFareInputDefault;
            } 
            else if (sessionManager.HasTimeBasedJourneyResults) 
            {
                return FindInputAdapter.GetHeaderTransitionEventFromMode(sessionManager.TimeBasedFindAMode);
            } 
            else 
            {
                return FindInputAdapter.GetHeaderTransitionEventFromMode(FindAMode.None);
            }
        }

        /// <summary>
        /// Returns true if the current Find A mode indicates a cost based search
        /// </summary>
        /// <returns>true if the current Find A mode indicates a cost based search,
        /// false otherwise</returns>
        public static bool IsCostBasedSearchMode(FindAMode mode)
        {
            return mode == FindAMode.Fare || mode == FindAMode.TrunkCostBased || mode == FindAMode.RailCost;
        }

        #endregion

    }
}
