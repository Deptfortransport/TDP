// *********************************************** 
// NAME                 : StopInformationPlanJourneyControl.ascx.cs
// AUTHOR               : Amit Patel
// DATE CREATED         : 10/09/2009 
// DESCRIPTION  		: Stop information plan a journey control
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/StopInformationPlanJourneyControl.ascx.cs-arc  $
//
//   Rev 1.5   Jan 04 2010 10:55:28   apatel
//Resolve the issue with Journey Planner defaults to midnight
//Resolution for 5352: The journey planner defaults to midnight - stop info page landing
//
//   Rev 1.4   Oct 23 2009 10:44:22   apatel
//make changes so stop location doesn't show as ambiguity on Journey Ambiguity page
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.3   Oct 15 2009 14:49:06   apatel
//Stop Information Departure Board Service code changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.2   Oct 06 2009 14:41:42   apatel
//Stop Information code changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.1   Sep 14 2009 15:15:48   apatel
//updated header logging
//Resolution for 5315: CCN526 Stop Information Page Landing

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.ScreenFlow;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using Logger = System.Diagnostics.Trace;
using TransportDirect.UserPortal.Resource;
using System.Globalization;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Stop information plan a journey user control
    /// </summary>
    public partial class StopInformationPlanJourneyControl : TDUserControl
    {
        #region Private fields
        // Session Managers
        private ITDSessionManager sessionManager;
        private TDItineraryManager itineraryManager;

        private bool stopInfoMode = false;
        private TDNaptan naptan; 
        private string stopName = string.Empty;
        private TDStopType stopType;

        // Declaration of search/location object members
        private LocationSearch originSearch = new LocationSearch();
        private LocationSearch destinationSearch = new LocationSearch();
        private TDLocation destinationLocation = new TDLocation();
        private TDLocation originLocation = new TDLocation();

        // Data Services
        private IDataServices populator;
        private IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
        #endregion

        #region Properties
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
                    sessionManager.SetOneUseKey(SessionKey.PublicModesRequired, "true");
                }
                else
                {
                    // Null the journeyParameters.PublicModes values so no modes of transport are checked.
                    sessionManager.JourneyParameters.PublicModes = new TransportDirect.JourneyPlanning.CJPInterface.ModeType[0];
                    sessionManager.SetOneUseKey(SessionKey.PublicModesRequired, "false");
                }
            }
        }

        /// <summary>
        /// Gets/Sets Hour
        /// </summary>
        public string Hour
        {
            get
            {
                return listHours.SelectedItem.Value;
            }

            set
            {
                // if not a number then it's should be the last index selected (no selection "-")
                int hour;
                try
                {
                    hour = Convert.ToInt32(value, CultureInfo.CurrentCulture.NumberFormat);
                }
                catch
                {
                    listHours.SelectedIndex = listHours.Items.Count - 1;
                    return;
                }

                listHours.SelectedIndex = hour;

            }
        }

        /// <summary>
        /// Gets/Sets Minute
        /// </summary>
        public string Minute
        {
            get
            {
                return listMinutes.SelectedItem.Value;
            }
            set
            {

                // if not a number then it's should be the last index selected (no selection "-")
                int minute;
                try
                {
                    minute = Convert.ToInt32(value, CultureInfo.CurrentCulture.NumberFormat);
                }
                catch
                {
                    listMinutes.SelectedIndex = listMinutes.Items.Count - 1;
                    return;
                }

                listMinutes.SelectedIndex = minute / 5; // div/5 because increments of 5
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
            this.buttonSubmit.Click += new EventHandler(this.buttonSubmit_Click);
            this.okButton.Click += new EventHandler(okButton_Click);

        }

        /// <summary>
        /// ok button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void okButton_Click(object sender, EventArgs e)
        {
            if (ToOption.Checked)
            {
                labelTo.Attributes["style"] = "display:none;";
                labelFrom.Attributes["style"] = "";
            }
            else
            {
                labelTo.Attributes["style"] = "";
                labelFrom.Attributes["style"] = "display:none;";
            }
        }
        #endregion	

        #region Page Events
        /// <summary>
        /// Page Initialise Method
        /// </summary>
        protected void Page_Init(object sender, System.EventArgs e)
        {
            // Populating the dropdown lists
            populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
        }

        /// <summary>
        /// Page load event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Create new session and itinerary managers
            sessionManager = TDSessionManager.Current;
            itineraryManager = TDItineraryManager.Current;

            if (!IsPostBack)
            {
                labelStopName.Text = stopName;
            }

            UpdateControl();

            
            if (((TDPage)Page).IsJavascriptEnabled)
            {
                ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
                // Output reference to necessary JavaScript file from the ScriptRepository
                Page.ClientScript.RegisterClientScriptBlock(typeof(StopInformationPlanJourneyControl), "Common", scriptRepository.GetScript("Common", ((TDPage)Page).JavascriptDom));
            }
            

            // Attempt to hide OK button using clientside JavaScript
            JavaScriptAdapter.InitialiseControlVisibility(okButton, false);



        }
        #endregion

        #region Control Events


        /// <summary>
        /// Event handler for clicks of the GO button
        /// </summary>
        protected void buttonSubmit_Click(object sender, System.EventArgs e)
        {

            // Clear Session Info
            ClearJourneySessionInfo();

            // Save Session Data
            SaveSessionInputData();

            // Perform ambiguity searches
            CallAmbiguitySearches();

            // Validate & search
            CallValidateAndSearch();

            //Log the submit event
            PageEntryEvent logpage = new PageEntryEvent(PageId.HomePageFindAJourneyPlanner, Session.SessionID, TDSessionManager.Current.Authenticated);
            Logger.Write(logpage);

        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Initialises the control
        /// </summary>
        /// <param name="stopInfoMode"></param>
        /// <param name="stopType"></param>
        /// <param name="naptan"></param>
        /// <param name="stopName"></param>
        public void initialize(bool stopInfoMode, TDStopType stopType, string naptan, string stopName)
        {
            this.stopInfoMode = stopInfoMode;

            this.stopType = stopType;

            NaptanCacheEntry x = NaptanLookup.Get(naptan, "Naptan");

            this.naptan = new TDNaptan(naptan, x.OSGR);
            
            this.stopName = stopName;
        }
        #endregion


        #region Private Methods

        /// <summary>
        /// Populates the labels/buttons with text from the Resource files
        /// </summary>
        private void UpdateControl()
        {
            // populate dropdown list for place type and show options			
            PopulateList();

            TDJourneyParametersMulti journeyParameters = sessionManager.JourneyParameters as TDJourneyParametersMulti;
            // Initialise PublicModes to blank list so as not use the InitialiseGeneric
            // method of TDJourneyParametersMulti which includes Car under PublicModes of transport
            if (journeyParameters != null)
            {
                journeyParameters.PublicModes = new ModeType[] { };
            }

            labelPlanAJourneyTitle.Text = GetResource("StopInformationPlanAJourneyControl.labelPlanAJourneyTitle.Text");

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
            imageDoorToDoor.ImageUrl = GetResource("StopInformationPlanAJourneyControl.imageDoorToDoor.ImageUrl");
            imageDoorToDoor.AlternateText = GetResource("StopInformationPlanAJourneyControl.imageDoorToDoor.AlternateText");
            hyperlinkDoorToDoor.ToolTip = GetResource("StopInformationPlanAJourneyControl.imageDoorToDoor.AlternateText");

            // Tool tips for Buttons
            buttonSubmit.ToolTip = GetResource("StopInformationPlanAJourneyControl.buttonSubmit.AlternateText");

            // Getting text for Labels
            FromOption.Text = GetResource("StopInformationPlanAJourneyControl.labelFrom.Text");
            labelFrom.Text = GetResource("StopInformationPlanAJourneyControl.labelFrom.Text");
            ToOption.Text = GetResource("StopInformationPlanAJourneyControl.labelTo.Text");
            labelTo.Text = GetResource("StopInformationPlanAJourneyControl.labelTo.Text");
            labelLeave.Text = GetResource("StopInformationPlanAJourneyControl.labelLeave.Text");
            labelShow.Text = GetResource("StopInformationPlanAJourneyControl.labelShow.Text");
            okButton.Text = GetResource("loginPanel.OkBtn.Text");
            

            // Setting screen reader field
            labelTravel.Text = GetResource("StopInformationPlanAJourneyControl.labelTravel.Text");
            
            // Getting text for CheckBoxes
            checkBoxPublicTransport.Text = GetResource("StopInformationPlanAJourneyControl.checkboxPublicTransport.Text");
            checkBoxCarRoute.Text = GetResource("StopInformationPlanAJourneyControl.checkboxCarRoute.Text");

            LeaveAfterOption.Text = GetResource("StopInformationPlanAJourneyControl.leaveAfterOption.Text");
            ArriveBeforeOption.Text = GetResource("StopInformationPlanAJourneyControl.arriveBeforeOption.Text");

            labelTravel.Text = GetResource("StopInformationPlanAJourneyControl.labelTravel.Text");

            buttonSubmit.Text = GetResource("StopInformationPlanAJourneyControl.buttonSubmit.Text");


            // Getting text for Labels
            FromOption.Text = GetResource("StopInformationPlanAJourneyControl.labelFrom.Text");
            ToOption.Text = GetResource("StopInformationPlanAJourneyControl.labelTo.Text");

            string optionScript = "toggleVisibility('" + labelTo.ClientID + "');toggleVisibility('" + labelFrom.ClientID + "');";

            FromOption.Attributes["onclick"] = optionScript;

            ToOption.Attributes["onclick"] = optionScript;

           
        }

        /// <summary>
        /// Populates the drop down list controls
        /// </summary>
        private void PopulateList()
        {
            // Populating LocationGazeteerOptions
            // origin DropDown
            populator.LoadListControl(
                DataServiceType.FindLocationGazeteerOptions, dropDownLocationGazeteerOptions, resourceManager);

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
            Hour = tempParameters.OutwardHour;
            Minute = tempParameters.OutwardMinute;

        }

        /// <summary>
        /// Determines the search type selected in the Gazateer
        /// </summary>
        private SearchType GetSearchType(DropDownList dropDownGazateer)
        {
            // Determine selected option from available Gazateer options
            string selectedGazOption;
            SearchType returnSearchType;
            selectedGazOption = dropDownGazateer.SelectedValue.ToString();
            FindLocationGazeteerOptions findLocationGazeteerOptions = GetFindLocationGazeteerOptions(selectedGazOption);

            // Set return search type according to the selected Gazateer option
            switch (findLocationGazeteerOptions)
            {
                case FindLocationGazeteerOptions.AttractionFacility:
                    returnSearchType = SearchType.POI;
                    break;
                case FindLocationGazeteerOptions.CityTownSuburb:
                    returnSearchType = SearchType.Locality;
                    break;
                case FindLocationGazeteerOptions.StationAirport:
                    returnSearchType = SearchType.MainStationAirport;
                    break;
                case FindLocationGazeteerOptions.AddressPostcode:
                default:
                    returnSearchType = SearchType.AddressPostCode;
                    break;
            }

            return returnSearchType;
        }

        /// <summary>
        /// Returns Gazateer Location options
        /// </summary>
        private FindLocationGazeteerOptions GetFindLocationGazeteerOptions(string listValue)
        {
            return (FindLocationGazeteerOptions)Enum.Parse(typeof(FindLocationGazeteerOptions), listValue);
        }

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
            sessionManager.IsStopInformationMode = stopInfoMode;

            TDJourneyParametersMulti journeyParameters = (TDJourneyParametersMulti)sessionManager.JourneyParameters;

            SearchType searchType = SearchType.AllStationStops;

            if (stopType == TDStopType.Air || stopType == TDStopType.Rail)
            {
                searchType = SearchType.MainStationAirport;
            }

            NaptanCacheEntry x = NaptanLookup.Get(naptan.Naptan, "Naptan");

            // Set up origin Location and Search Type values
            originLocation.SearchType =  FromOption.Checked? searchType : GetSearchType(dropDownLocationGazeteerOptions);
            if (FromOption.Checked)
            {

                originLocation.NaPTANs = new TDNaptan[] {naptan };
                originLocation.Status = TDLocationStatus.Valid;
                originLocation.GridReference = x.OSGR;

                originLocation.Locality = x.Locality;

                originLocation.Description = stopName;
                journeyParameters.OriginLocation.Status = TDLocationStatus.Valid;
            }

            originSearch.InputText = FromOption.Checked? labelStopName.Text : textFromTo.Text;

            originSearch.SearchType = originLocation.SearchType;
            // Save them to the JourneyParameters
            journeyParameters.OriginLocation.SearchType = originLocation.SearchType;
            journeyParameters.Origin.SearchType = originLocation.SearchType;
            journeyParameters.Origin.InputText = originSearch.InputText;
            journeyParameters.Origin.LocationFixed = FromOption.Checked;
            journeyParameters.OriginLocation.NaPTANs = originLocation.NaPTANs;
            journeyParameters.OriginLocation.GridReference = originLocation.GridReference;
            journeyParameters.OriginLocation.Locality = originLocation.Locality;
            journeyParameters.OriginLocation.Description = originLocation.Description;
                
            
            // Set up destination Location and Search Type values
            destinationLocation.SearchType = FromOption.Checked ? GetSearchType(dropDownLocationGazeteerOptions) : searchType;
            if (!FromOption.Checked)
            {
                destinationLocation.NaPTANs = new TDNaptan[] {naptan };
                destinationLocation.Status = TDLocationStatus.Valid;
                destinationLocation.GridReference = x.OSGR;

                destinationLocation.Locality = x.Locality;

                destinationLocation.Description = stopName;
                journeyParameters.DestinationLocation.Status = TDLocationStatus.Valid;
            }
            destinationSearch.InputText = FromOption.Checked ? textFromTo.Text : labelStopName.Text;
            destinationSearch.SearchType = destinationLocation.SearchType;
            // Save them to the JourneyParameters
            journeyParameters.DestinationLocation.SearchType = destinationLocation.SearchType;
            journeyParameters.Destination.SearchType = destinationLocation.SearchType;
            journeyParameters.Destination.InputText = destinationSearch.InputText;
            journeyParameters.Destination.LocationFixed = !FromOption.Checked;
            journeyParameters.DestinationLocation.NaPTANs = destinationLocation.NaPTANs;
            journeyParameters.DestinationLocation.GridReference = originLocation.GridReference;
            journeyParameters.DestinationLocation.Locality = originLocation.Locality;
            journeyParameters.DestinationLocation.Description = originLocation.Description;
             

            // Save selected date control values to the JourneyParameters
            journeyParameters.OutwardDayOfMonth = ambiguousDateSelectControl.Day.ToString();
            journeyParameters.OutwardMonthYear = ambiguousDateSelectControl.MonthYear.ToString();
            journeyParameters.OutwardHour = Hour.ToString();
            journeyParameters.OutwardMinute = Minute.ToString();

            journeyParameters.OutwardArriveBefore = !LeaveAfterOption.Checked;

            //Set the fuel cost-consumption
            JourneyPlannerInputAdapter.SetFuelCostConsumption(journeyParameters);

            // Save the Find Public Transport / Car Journeys check box to Journey Parameters
            journeyParameters.PublicModes = new ModeType[] { };
            journeyParameters.PublicRequired = checkBoxPublicTransport.Checked;
            journeyParameters.PrivateRequired = checkBoxCarRoute.Checked;

            // Set all Modes of transport depending on wether or not PublicTransport is required.
            this.PublicRequired = checkBoxPublicTransport.Checked;
        }

        /// <summary>
        /// Perform Ambiguity Searches
        /// </summary>
        private void CallAmbiguitySearches()
        {
            TDJourneyParametersMulti journeyParameters = (TDJourneyParametersMulti)sessionManager.JourneyParameters;

            // Set up AmbiguitySearch Objects
            TDSessionManager.Current.AmbiguityResolution = new AmbiguityResolutionState();
            TDSessionManager.Current.AmbiguityResolution.SaveJourneyParameters();

            // Perform and save Ambiguity search on origin location
            JourneyPlannerInputAdapter.AmbiguitySearch(ref originLocation, ref originSearch,
                journeyParameters, true, true);
            journeyParameters.OriginLocation = originLocation;
            journeyParameters.Origin = originSearch;

            // Perform and save Ambiguity search on destination location
            JourneyPlannerInputAdapter.AmbiguitySearch(ref destinationLocation, ref destinationSearch,
                journeyParameters, true, true);
            journeyParameters.DestinationLocation = destinationLocation;
            journeyParameters.Destination = destinationSearch;

            journeyParameters.SaveDetails = false;
        }

       

        /// <summary>
        /// Calls Validate and Search method to complete journey search
        /// </summary>
        private void CallValidateAndSearch()
        {
            // Perform Validate & Search
            JourneyPlannerInputAdapter adapt = new JourneyPlannerInputAdapter();
            adapt.ValidateAndSearch(true, TransportDirect.Common.PageId.JourneyPlannerInput);
        }
        #endregion
    }
}