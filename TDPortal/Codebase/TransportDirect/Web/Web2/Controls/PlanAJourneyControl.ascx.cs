// *********************************************** 
// NAME                 : PlanAJourneyControl.ascx
// AUTHOR               : Robert Griffith
// DATE CREATED         : 03/11/2005 
// DESCRIPTION  		: Home page Plan a journey control
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/PlanAJourneyControl.ascx.cs-arc  $ 
//
//   Rev 1.5   Aug 28 2012 11:55:52   mmodi
//Set default searchtype before transitioning to door to door for no location entered
//Resolution for 5832: CCN Gaz
//
//   Rev 1.4   Aug 28 2012 10:21:18   mmodi
//Added LocationSuggest functionality for journey planners (d2d, cycle, car)
//Resolution for 5832: CCN Gaz
//
//   Rev 1.3   Apr 21 2008 14:03:20   mturner
//IR4882 - Selected Index of Time dropdows not being preserved. 
//
//   Rev 1.2   Mar 31 2008 13:22:24   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:16:56   mturner
//Initial revision.
//
//   Rev 1.24   Mar 09 2006 11:49:10   esevern
//corrected PageId used when logging PageEntryEvent
//Resolution for 3586: Additional PageEntryEvents on HomePage
//
//   Rev 1.23   Feb 24 2006 17:25:16   build
//Removed duplicate namespace reference
//
//   Rev 1.22   Feb 23 2006 16:13:10   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.21   Feb 20 2006 14:24:48   esevern
//added logging of PageEntryEvent on submit
//Resolution for 3586: Additional PageEntryEvents on HomePage
//
//   Rev 1.20   Feb 17 2006 16:18:24   tolomolaiye
//Fix for IR 3572. Fixed error that occured when planning a Door-to-Door journey immediately after planning a Vist Planner (Day Trip Planner) journey.
//Resolution for 3572: Del 8.1 - H2 Merge - Problems with JourneySummary, Journey Details, JourneyMaps, and JourneyTickets pages.
//
//   Rev 1.19   Feb 10 2006 18:09:28   kjosling
//Fixed merge 
//
//   Rev 1.19   Feb 10 2006 18:05:22   kjosling
//Fixed merge
//
//   Rev 1.19   Feb 10 2006 17:59:42   kjosling
//Fixed
//
//   Rev 1.18   Feb 10 2006 12:24:48   tolomolaiye
//Merge for stream 3180 - Homepage Phase 2
//
//   Rev 1.17   Jan 11 2006 12:17:28   RPhilpott
//Temporarily mask Find-A mode state when populating control with new journey parameters so that door-to-door values are used.
//Resolution for 3443: Del 8 - Door-to-Door input on home page shows wrong time
//
//   Rev 1.16   Jan 11 2006 10:38:42   mguney
//Fuel cost and consumption included to the journey parameters in SaveSessionInputData method.
//Resolution for 3439: Homepage: Mini Journey planner does not include car journeys
//
//   Rev 1.15   Jan 10 2006 15:40:28   RGriffith
//Added Tooltip for the Advanced button as recommended in WAI reveiw
//Resolution for 3440: Del 8: WAI Review Changes
//
//   Rev 1.14   Jan 05 2006 11:17:00   AViitanen
//Changes to enable Public Modes to be correctly populated when not selecting to search for car journeys.
//Resolution for 3411: Del 8: Homepage - mini journey planner returns car journeys when car is deselected
//
//   Rev 1.13   Dec 21 2005 17:11:48   jgeorge
//Defer calling InitialiseJourneyParametersPageStates until the user clicks the "Advanced" or "Go" buttons. This enables the Door to Door input page to load preferences when the user navigates to the page.
//
//Also added code to use saved preferences if user is authenticated when they plan a journey from the homepage.
//Resolution for 3362: PT & car preferences not stored correctly when "Save these details" checked
//
//   Rev 1.12   Dec 20 2005 11:13:32   jgeorge
//Modified population code to ensure correct date/time displayed
//Resolution for 3370: Navigation Error in Find a Fare
//
//   Rev 1.11.1.3   Jan 11 2006 13:55:30   tmollart
//Updated after comments from code review. Removed code that sets the tab section.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.11.1.2   Jan 04 2006 14:14:48   RGriffith
//Changes to enable Public Modes to be correctly populated when not selecting to seach for Car journeys
//
//   Rev 1.11.1.1   Dec 23 2005 14:26:00   RGriffith
//Changes to use TDImage
//
//   Rev 1.11.1.0   Dec 15 2005 09:52:06   tmollart
//Changed to use InitialiseJourneyParametes.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.11   Dec 06 2005 16:40:20   tmollart
//Modified how journey parameters are initialised.
//Resolution for 3208: UEE Homepage: Cannot plan a journey using the homepage (locations disappear from ambiguity)
//
//   Rev 1.10   Nov 28 2005 11:35:08   tmollart
//Changed Page ID passed through to control where user returns to after the ambiguity page Back button is pressed.
//Resolution for 3209: UEE Hompage:  After using the Homepage miniplanner and reaching ambiguity page, pressing "back" returns you to the Hompage when it should not
//
//   Rev 1.9   Nov 26 2005 12:49:46   tmollart
//Modified ClearJourneySessionInfo so that it calls ResetItinerary and not NewSearch.
//Resolution for 3212: UEE Homepage:  Entering locations and pressing Advanced takes user to a blank Door to Door page with no values passed
//
//   Rev 1.8   Nov 18 2005 14:31:10   RGriffith
//Fix to ensure JourneyPlannerInput TransportModes checkboxes are correctly populated
//
//   Rev 1.7   Nov 14 2005 13:16:26   pcross
//Fix to ensure link from Plan a Journey icon always goes to Door to Door (not a FindA page)
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.6   Nov 14 2005 13:10:58   RGriffith
//FxCop changes
//
//   Rev 1.5   Nov 14 2005 11:26:24   pcross
//Replaced GetString calls with more recent method: GetResource
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.4   Nov 11 2005 14:17:32   pcross
//Added alt text
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.3   Nov 11 2005 09:52:08   RGriffith
//Further changes to conform to requirements
//
//   Rev 1.2   Nov 10 2005 17:48:30   pcross
//Minor updates
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.1   Nov 10 2005 15:03:34   RGriffith
//Plan A Journey Control Updates to meet design
//
//   Rev 1.0   Nov 07 2005 15:44:30   RGriffith
//	Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
    using System;
    using System.Web.UI.WebControls;
    using TransportDirect.Common;
    using TransportDirect.Common.ServiceDiscovery;
    using TransportDirect.JourneyPlanning.CJPInterface;
    using TransportDirect.ReportDataProvider.TDPCustomEvents;
    using TransportDirect.UserPortal.DataServices;
    using TransportDirect.UserPortal.LocationService;
    using TransportDirect.UserPortal.Resource;
    using TransportDirect.UserPortal.ScreenFlow;
    using TransportDirect.UserPortal.SessionManager;
    using TransportDirect.UserPortal.Web;
    using TransportDirect.UserPortal.Web.Adapters;
    using Logger = System.Diagnostics.Trace;

	/// <summary>
	///	Summary description for FindAPlaceControl.
	/// </summary>
	public partial class PlanAJourney : TDUserControl
    {
        #region Private members

        #region Control Declarations

        protected TransportDirect.UserPortal.Web.Controls.AmbiguousDateSelectControl ambiguousDateSelectControl;
		
		#endregion

		#region Object Members Declarations

		// Session Managers
		ITDSessionManager sessionManager;
		TDItineraryManager itineraryManager;

		// Declaration of search/location object members
		private LocationSearch originSearch = new LocationSearch();
		private LocationSearch destinationSearch = new LocationSearch();
		private TDLocation destinationLocation = new TDLocation();
		private TDLocation originLocation = new TDLocation();

		// Data Services
		protected IDataServices populator;
		private IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

        #endregion

        #endregion

        #region Page_Init, Page_Load

        /// <summary>
        /// Page Initialise Method
        /// </summary>
        protected void Page_Init(object sender, System.EventArgs e)
        {
            // Populating the dropdown lists
            populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
        }

        /// <summary>
		/// Page Load Method
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
            // Create new session and itinerary managers
			sessionManager = TDSessionManager.Current;			
			itineraryManager = TDItineraryManager.Current;

            //We need to persist values in the drop down lists since
            //the update control call will clear them. This is as a 
            //result of the white labelling...
            int minuteSelectedIndex = 0;
            int hourSelectedIndex = 0;
            int daySelectedIndex = 0;
            int monthSelectedIndex = 0;

            TDDateTime date = ambiguousDateSelectControl.Current;

            if(Page.IsPostBack)
            {
                minuteSelectedIndex = ambiguousDateSelectControl.ControlMinutes.SelectedIndex;
                hourSelectedIndex = ambiguousDateSelectControl.ControlHours.SelectedIndex;
                daySelectedIndex = ambiguousDateSelectControl.ControlDays.SelectedIndex;
                monthSelectedIndex = ambiguousDateSelectControl.ControlMonths.SelectedIndex;
            }

            // Initialise the location input controls
            InitialiseLocationControls();
                       
			// Update control for dropdown list, image path, soft content etc
			UpdateControl();

            if (Page.IsPostBack)
            {     
                ambiguousDateSelectControl.ControlMinutes.SelectedIndex = minuteSelectedIndex;
                ambiguousDateSelectControl.ControlHours.SelectedIndex = hourSelectedIndex;
                ambiguousDateSelectControl.ControlDays.SelectedIndex = daySelectedIndex; 
                ambiguousDateSelectControl.ControlMonths.SelectedIndex = monthSelectedIndex;
            }
            else
            {
                ambiguousDateSelectControl.Current = date;
            }
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// Event handler for clicks of the Advanced button
        /// </summary>
        protected void buttonAdvance_Click(object sender, System.EventArgs e)
        {
            // Clear Session Info
            ClearJourneySessionInfo();

            // Save Session Data
            SaveSessionInputData();

            // Set session info to display advanced options
            SetAdvancedSessionInfo();

            // Transfer to JourneyPlannerInput with advanced options
            sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.PlanAJourneyAdvanced;
        }

        /// <summary>
        /// Event handler for clicks of the GO button
        /// </summary>
        protected void buttonSubmit_Click(object sender, System.EventArgs e)
        {
            // Clear Session Info
            ClearJourneySessionInfo();

            // Save Session Data
            SaveSessionInputData();

            // Validate locations and submit
            SubmitRequest();

            //Log the submit event
            PageEntryEvent logpage = new PageEntryEvent(PageId.HomePageFindAJourneyPlanner, Session.SessionID, TDSessionManager.Current.Authenticated);
            Logger.Write(logpage);
        }

        #endregion

        #region Private methods

        #region Session methods

        /// <summary>
		/// Method used to clear the JourneySessionInfo
		/// </summary>
		private void ClearJourneySessionInfo()
		{
			// Reset the itinerary manager
			itineraryManager.ResetItinerary();

			//set page result as invalid if previous results have been planned
			if (sessionManager.JourneyResult != null) 
			{ 
				sessionManager.JourneyResult.IsValid = false; 
			} 
			
			sessionManager.InitialiseJourneyParameters(FindAMode.None);

			if (sessionManager.Authenticated)
			{
				TDJourneyParametersMulti journeyParameters = sessionManager.JourneyParameters as TDJourneyParametersMulti;
				UserPreferencesHelper.LoadCarPreferences(journeyParameters);
				UserPreferencesHelper.LoadPublicTransportPreferences(journeyParameters);
			}
		}

		/// <summary>
		/// Saves the session data to be transferred to the JourneyPlannerInputPage
		/// </summary>
		private void SaveSessionInputData()
		{
			TDJourneyParametersMulti journeyParameters = (TDJourneyParametersMulti)sessionManager.JourneyParameters;

            // Location and Search Type values are set here, 
            // but if ValidateLocations called then they are updated with the validated control location and search
            journeyParameters.Origin = originLocationControl.Search;
            journeyParameters.Origin.InputText = originLocationControl.LocationInputText;
            journeyParameters.Origin.SearchType = originLocationControl.SearchTypeSelected;
            journeyParameters.OriginLocation = originLocationControl.Location;

            journeyParameters.Destination = destinationLocationControl.Search;
            journeyParameters.Destination.InputText = destinationLocationControl.LocationInputText;
            journeyParameters.Destination.SearchType = destinationLocationControl.SearchTypeSelected;
            journeyParameters.DestinationLocation = destinationLocationControl.Location;

            // If no location entered, then update search type to be the default for DoorToDoor
            if (string.IsNullOrEmpty(journeyParameters.Origin.InputText))
            {
                journeyParameters.Origin.SearchType = SearchType.MainStationAirport;
            }
            if (string.IsNullOrEmpty(journeyParameters.Destination.InputText))
            {
                journeyParameters.Destination.SearchType = SearchType.MainStationAirport;
            }

			
			// Save selected date control values to the JourneyParameters
			journeyParameters.OutwardDayOfMonth = ambiguousDateSelectControl.Day.ToString();
			journeyParameters.OutwardMonthYear = ambiguousDateSelectControl.MonthYear.ToString();
			journeyParameters.OutwardHour = ambiguousDateSelectControl.Hour.ToString();
			journeyParameters.OutwardMinute = ambiguousDateSelectControl.Minute.ToString();

			//Set the fuel cost-consumption
			JourneyPlannerInputAdapter.SetFuelCostConsumption(journeyParameters);

			// Save the Find Public Transport / Car Journeys check box to Journey Parameters
			journeyParameters.PublicModes = new ModeType[]{};
			journeyParameters.PublicRequired = checkBoxPublicTransport.Checked;
			journeyParameters.PrivateRequired = checkBoxCarRoute.Checked;

			// Set all Modes of transport depending on wether or not PublicTransport is required.
			this.PublicRequired = checkBoxPublicTransport.Checked;
		}

		/// <summary>
		/// Sets session info to view advanced options on JourneyPlannerInput page
		/// </summary>
		private void SetAdvancedSessionInfo()
		{
			// Ensure full set of advanced options are visible
			//		View Public Transport Options
			sessionManager.InputPageState.PublicTransportOptionsVisible = true;
			sessionManager.InputPageState.PublicTransportTypesVisible = true;
			//		View Car Options
			sessionManager.InputPageState.CarOptionsVisible = true;
			//		Set advanced options visible
			sessionManager.InputPageState.AdvancedOptionsVisible = true;

            // Ensure journey input page displays the location more options expanded
            this.LocationMoreOptions = true;
        }

        #endregion

        #region Submit

        /// <summary>
        /// Validates the location inputs and saves to the journey parameters
        /// </summary>
        private void ValidateLocations()
        {
            TDJourneyParametersMulti journeyParameters = (TDJourneyParametersMulti)sessionManager.JourneyParameters;

            #region Origin and Destination locations

            // Validate the location controls, this calls the location search

            // Origin
            originLocationControl.Validate(journeyParameters, true, true, true, StationType.Undetermined);

            journeyParameters.Origin = originLocationControl.Search;
            journeyParameters.OriginLocation = originLocationControl.Location;

            // Destination 
            destinationLocationControl.Validate(journeyParameters, true, true, true, StationType.Undetermined);

            journeyParameters.Destination = destinationLocationControl.Search;
            journeyParameters.DestinationLocation = destinationLocationControl.Location;

            #endregion
        }

        /// <summary>
        /// Validates inputs and submits the journey request
        /// </summary>
        private void SubmitRequest()
        {
            TDJourneyParametersMulti journeyParameters = (TDJourneyParametersMulti)sessionManager.JourneyParameters;

            ValidateLocations();

            // Save journey parameters that may change during ambiguity resolution
            TDSessionManager.Current.AmbiguityResolution = new AmbiguityResolutionState();
            TDSessionManager.Current.AmbiguityResolution.SaveJourneyParameters();

            // Reset it to false by default, do not want to save user preferences
            journeyParameters.SaveDetails = false;

            // Perform Validate & Search
            JourneyPlannerInputAdapter journeyPlannerInputAdapter = new JourneyPlannerInputAdapter();
            journeyPlannerInputAdapter.ValidateAndSearch(true, TransportDirect.Common.PageId.JourneyPlannerInput);
        }

        #endregion

        /// <summary>
        /// Initialises the origin, destination location controls
        /// </summary>
        private void InitialiseLocationControls()
        {
            // Populate always, the location control deals with any changed locations or not
            originLocationControl.Initialise(originLocation, originSearch, DataServiceType.FromToDrop, true, false, false, false, false, false, true, false, false);
            destinationLocationControl.Initialise(destinationLocation, destinationSearch, DataServiceType.FromToDrop, true, false, false, false, false, false, true, false, false);
        }

        /// <summary>
        /// Populates the labels/buttons with text from the Resource files
        /// </summary>
        private void UpdateControl()
        {
            // Populate dropdown list for place type and show options			
            PopulateList();

            TDJourneyParametersMulti journeyParameters = sessionManager.JourneyParameters as TDJourneyParametersMulti;
            // Initialise PublicModes to blank list so as not use the InitialiseGeneric
            // method of TDJourneyParametersMulti which includes Car under PublicModes of transport
            if (journeyParameters != null)
            {
                journeyParameters.PublicModes = new ModeType[] { };
            }

            // Assign URLs to hyperlinks
            IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];
            string baseChannel = String.Empty;
            string url = String.Empty;
            if (TDPage.SessionChannelName != null)
                baseChannel = TDPage.getBaseChannelURL(TDPage.SessionChannelName);

            // Hyperlink Transfer details for DoorToDoor Hyperlink
            PageTransferDetails pageTransferDetails = pageController.GetPageTransferDetails(PageId.JourneyPlannerInput);
            url = baseChannel + pageTransferDetails.PageUrl;
            hyperlinkDoorToDoor.NavigateUrl = url + "?DoorToDoor=true";

            // Updatng Image url path & AlternateText
            imageDoorToDoor.ImageUrl = GetResource("PlanAJourneyControl.imageDoorToDoor.ImageUrl");
            imageDoorToDoor.AlternateText = GetResource("PlanAJourneyControl.imageDoorToDoor.AlternateText");
            hyperlinkDoorToDoor.ToolTip = GetResource("PlanAJourneyControl.imageDoorToDoor.AlternateText");

            // Getting text for Labels
            labelFrom.Text = GetResource("PlanAJourneyControl.labelFrom.Text");
            labelTo.Text = GetResource("PlanAJourneyControl.labelTo.Text");
            labelLeave.Text = GetResource("PlanAJourneyControl.labelLeave.Text");
            labelShow.Text = GetResource("PlanAJourneyControl.labelShow.Text");

            // Setting location input text
            originLocationControl.LocationInput.ToolTip = GetResource("LocationControl.LocationInput.PlanAJourney.ToolTip");
            originLocationControl.LocationInputDescription.Text = GetResource("originSelect.labelSRLocation");
            originLocationControl.LocationTypeDescription.Text = GetResource("PlanAJourneyControl.labelFromPlaceTypeScreenReader.Text");

            destinationLocationControl.LocationInput.ToolTip = GetResource("LocationControl.LocationInput.PlanAJourney.ToolTip");
            destinationLocationControl.LocationInputDescription.Text = GetResource("destinationSelect.labelSRLocation");
            destinationLocationControl.LocationTypeDescription.Text = GetResource("PlanAJourneyControl.labelToPlaceTypeScreenReader.Text");

            // Getting text for CheckBoxes
            checkBoxPublicTransport.Text = GetResource("PlanAJourneyControl.checkboxPublicTransport.Text");
            checkBoxCarRoute.Text = GetResource("PlanAJourneyControl.checkboxCarRoute.Text");

            // Getting text for TDButtons
            buttonAdvanced.Text = GetResource("PlanAJourneyControl.MoreOptions.Text");
            buttonAdvanced.ToolTip = GetResource("PlanAJourneyControl.MoreOptions.ToolTip");
            buttonSubmit.Text = GetResource("PlanAJourneyControl.buttonSubmit.Text");
            buttonSubmit.ToolTip = GetResource("PlanAJourneyControl.buttonSubmit.AlternateText");
        }

        /// <summary>
        /// Populates the drop down list controls
        /// </summary>
        private void PopulateList()
        {            
            // Populate AmbiguousDateSelectControl
            ambiguousDateSelectControl.Populate();

            // The values for leaving date and time should not come from the current session journey
            // parameters, as these may have been set through a previous door to door search. We can't
            // just overwrite those parameters, as they may click the Door to Door tab to see their
            // results again. Therefore in order to generate the values for these dropdowns, create a new
            // TDJourneyParametersMulti instance (which will use the default date/time functionality)
            // and use that.

            // Temporarily "pretend" we are not in FindA mode, even if we are, so 
            //  that parameters will be set correctly for a new door-to-door search  

            FindPageState savePageState = TDSessionManager.Current.FindPageState;
            TDSessionManager.Current.FindPageState = null;

            TDJourneyParametersMulti tempParameters = new TDJourneyParametersMulti();

            TDSessionManager.Current.FindPageState = savePageState;

            ambiguousDateSelectControl.Day = tempParameters.OutwardDayOfMonth;
            ambiguousDateSelectControl.MonthYear = tempParameters.OutwardMonthYear;
            ambiguousDateSelectControl.Hour = tempParameters.OutwardHour;
            ambiguousDateSelectControl.Minute = tempParameters.OutwardMinute;
        }

        #endregion

        #region Private properties
        
        /// <summary>
		/// Sets Public required
		/// Used for determining the modes of public transport required on the JounreyPlannerInput page.
		/// </summary>
		private bool PublicRequired
		{
			set
			{
				if (value)
				{
					// Add a oneuse session key to enable the JounreyPlannerInput page to set the 
					//   journeyParameters.PublicModes so ALL transport modes are checked.
					sessionManager.SetOneUseKey(SessionKey.PublicModesRequired,"true");
				}
				else
				{
					// Null the journeyParameters.PublicModes values so no modes of transport are checked.
					sessionManager.JourneyParameters.PublicModes = new TransportDirect.JourneyPlanning.CJPInterface.ModeType[0];
					sessionManager.SetOneUseKey(SessionKey.PublicModesRequired,"false");
				}
			}
        }

        /// <summary>
        /// Sets More options expanded flag for the location controls
        /// </summary>
        private bool LocationMoreOptions
        {
            set
            {
                if (value)
                {
                    // Add a oneuse session key to enable the JounreyPlannerInput page to set the 
                    // locations controls to display the more options
                    sessionManager.SetOneUseKey(SessionKey.ExpandOptionsRequired, "true");
                }
                else
                {
                    sessionManager.SetOneUseKey(SessionKey.ExpandOptionsRequired, "false");
                }
            }
        }

        #endregion

        #region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.buttonAdvanced.Click += new EventHandler(this.buttonAdvance_Click);
            this.buttonSubmit.Click += new EventHandler(this.buttonSubmit_Click);
  		}
		#endregion	   
    }
}
