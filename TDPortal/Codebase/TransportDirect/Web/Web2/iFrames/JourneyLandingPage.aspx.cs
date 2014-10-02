// ***********************************************
// NAME         : JourneyLandingPage.aspx.cs
// AUTHOR       : Darshan Sawe
// DATE CREATED : 
// DESCRIPTION  : Journey landing page for use by the journey planning iFrame. Accepts a place name and gazetteer type
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/iFrames/JourneyLandingPage.aspx.cs-arc  $
//
//   Rev 1.3   Oct 07 2010 14:42:50   apatel
//Updated to resolve the monthyear value issue with google desktop gadget
//Resolution for 5618: Issue with google gadget and bussiness link template with month having extra 0 at bigining
//
//   Rev 1.2   Aug 24 2009 15:16:32   mmodi
//Updated logic to handle missing parameters and auto identify location type
//Resolution for 5311: CCN532 Page Landing for Bing
//
//   Rev 1.1   Jun 23 2009 14:41:14   mmodi
//Updated to support the advanced button, used by the Door to door Gadget
//Resolution for 5300: CCN0482 Door to door Gadget

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
  
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
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
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.Web.Support;

using ControlPopulator = TransportDirect.UserPortal.DataServices.DataServices;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// This is the Landing for iframe Journey Planning Mini-Page
	/// </summary>
	public partial class JourneyLandingPage : TDPage
	{
		#region Object Members Declarations
		// Session Managers
		ITDSessionManager sessionManager;
		TDItineraryManager itineraryManager;

		// Declaration of search/location object members
		private LocationSearch originSearch = new LocationSearch();
		private LocationSearch destinationSearch = new LocationSearch();
		private TDLocation destinationLocation = new TDLocation();
		private TDLocation originLocation = new TDLocation();

		// Private variables to store query string variables
		private string fromDropDownLocationGazeteerOptions = String.Empty;
		private string textBoxFromText = String.Empty;
		private string toDropDownLocationGazeteerOptions = String.Empty;
		private string textBoxToText = String.Empty;
		private string day = String.Empty;
		private string monthYear = String.Empty;
		private string hour = String.Empty;
		private string minute = String.Empty;
		private string partnerId = String.Empty;
        private bool checkBoxPublicTransport = true;
		private bool checkBoxCarRoute = true;
        private bool showAdvanced = false;
        private bool autoPlan = true;

        private bool fromGazSpecified = true;
        private bool toGazSpecified = true;
        private bool ptModeSpecified = true;
        private bool carModeSpecified = true;
        private bool showAdvancedSpecified = true;
        private bool autoPlanSpecified = true;

        // Constants - request parameter names
        private const string paramFromGaz = "from";
        private const string paramFromText = "txtFrom";
        private const string paramToGaz = "to";
        private const string paramToText = "txtTo";
        private const string paramDay = "day";
        private const string paramMonthYear = "monYr";
        private const string paramHour = "hr";
        private const string paramMinute = "min";
        private const string paramPublicTransport = "public";
        private const string paramCar = "car";
        private const string paramAdvanced = "advanced";
        private const string paramPartnerId = "pid";
        private const string paramAutoPlan = "p";

		#endregion

        #region Constructor

        /// <summary>
        /// Page constructor
        /// </summary>
		public JourneyLandingPage()
		{
			this.pageId = TransportDirect.Common.PageId.JourneyLandingPage;
        }

        #endregion

        #region Page_Load

        /// <summary>
        /// Page_Load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			
			// Create new session and itinerary managers
			sessionManager = TDSessionManager.Current;			
			itineraryManager = TDItineraryManager.Current;

			
			// Save query string variables
			SaveQueryString();

            // Update parameters for the partner
            UpdateQueryParametersForPartner();

			// Clear Session Info
			ClearJourneySessionInfo();

			// Save Session Data
			SaveSessionInputData();

            // Show the advanced page or perform the journey planner search
            if (!showAdvanced)
            {
                // Ensure advanced options are not shown
                SetAdvancedSessionInfo(false);

                // Only resolve and plan journey if auto plan is set to true (true by default)
                if (autoPlan)
                {
                    // Perform ambiguity searches
                    CallAmbiguitySearches();

                    // Validate & search
                    CallValidateAndSearch();
                }
            }
            else
            {
                // Set session info to display advanced options
                SetAdvancedSessionInfo(true);

                // Perform Transition to JourneyPlannerInput with advanced options
                PerformTransitionToJourneyPlannerAdvanced();
            }

			//Mis logging				
			LandingPageEntryEvent lpee = new LandingPageEntryEvent(partnerId, LandingPageService.iFrameJourneyLandingPage, Session.SessionID, TDSessionManager.Current.Authenticated);
			Logger.Write(lpee);

            // If not autoplanning the journey, take user to the door to door input page
            if (!autoPlan)
            {
                string pageURL = string.Empty;

                if (TDPage.SessionChannelName != null)
                {
                    pageURL = getBaseChannelURL(TDPage.SessionChannelName);
                }

                //create a page controller object in order to get the page transfer details
                IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];
                PageTransferDetails pageTransferDetails = pageController.GetPageTransferDetails(PageId.JourneyPlannerInput);

                pageURL += pageTransferDetails.PageUrl;

                //Add system time to end of redirect URL (fake querystring param). This ensures latest version of page is requested rather than relying on cache
                pageURL += "?x=" + Server.UrlEncode(DateTime.UtcNow.ToLongTimeString());

                Response.Redirect(pageURL);
            }
        }

        #endregion

        #region Private methods

        /// <summary>
		/// saving Query String received from iFrameJourneyPlanningControl
		/// </summary>
		private void SaveQueryString()
		{
            // Set if values exist
            this.fromGazSpecified = DoesParamExist(paramFromGaz);
            this.toGazSpecified = DoesParamExist(paramToGaz);
            this.ptModeSpecified = DoesParamExist(paramPublicTransport);
            this.carModeSpecified = DoesParamExist(paramCar);
            this.showAdvancedSpecified = DoesParamExist(paramAdvanced);
            this.autoPlanSpecified = DoesParamExist(paramAutoPlan);

            // Get values (any parameters not specified will be set to string.empty or true/false)
			this.fromDropDownLocationGazeteerOptions = Server.HtmlDecode(GetValidParam(paramFromGaz));
			this.textBoxFromText = Server.HtmlDecode(GetValidParam(paramFromText));
			
			this.toDropDownLocationGazeteerOptions = Server.HtmlDecode(GetValidParam(paramToGaz));
			this.textBoxToText = Server.HtmlDecode(GetValidParam(paramToText));

			this.day = Server.HtmlDecode(GetValidParam(paramDay));
			this.monthYear = Server.HtmlDecode(GetValidParam(paramMonthYear));
			this.hour = Server.HtmlDecode(GetValidParam(paramHour));
            this.minute = Server.HtmlDecode(GetValidParam(paramMinute));

			this.checkBoxPublicTransport = GetBooleanSingleParam(paramPublicTransport);
            this.checkBoxCarRoute = GetBooleanSingleParam(paramCar);

            this.showAdvanced = GetBooleanSingleParam(paramAdvanced);
            this.autoPlan = GetBooleanSingleParam(paramAutoPlan);

			this.partnerId = Server.HtmlDecode(GetValidParam(paramPartnerId));

            #region Log output
            if (TDTraceSwitch.TraceVerbose)
            {
                StringBuilder queryParameters = new StringBuilder();
                
                queryParameters.Append(LandingPageService.iFrameJourneyLandingPage.ToString());
                queryParameters.Append("request parameters: ");
                
                queryParameters.Append(string.Format("From[{0}] ", textBoxFromText));
                queryParameters.Append(string.Format("FromGaz[{0}] ", fromDropDownLocationGazeteerOptions));
                queryParameters.Append(string.Format("To[{0}] ", textBoxToText));
                queryParameters.Append(string.Format("ToGaz[{0}] ", toDropDownLocationGazeteerOptions));
                queryParameters.Append(string.Format("Day[{0}] ", day));
                queryParameters.Append(string.Format("MonthYear[{0}] ", monthYear));
                queryParameters.Append(string.Format("Hour[{0}] ", hour));
                queryParameters.Append(string.Format("Minute[{0}] ", minute));
                queryParameters.Append(string.Format("PT[{0}] ", (ptModeSpecified ? checkBoxPublicTransport.ToString() : string.Empty)));
                queryParameters.Append(string.Format("Car[{0}] ", (carModeSpecified ? checkBoxCarRoute.ToString() : string.Empty)));
                queryParameters.Append(string.Format("ShowAdvanced[{0}] ", (showAdvancedSpecified ? showAdvanced.ToString() : string.Empty)));
                queryParameters.Append(string.Format("AutoPlan[{0}] ", (autoPlanSpecified ? autoPlan.ToString() : string.Empty)));
                queryParameters.Append(string.Format("PartnerId[{0}]", partnerId));

                OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, sessionManager.Session.SessionID, TDTraceLevel.Verbose, queryParameters.ToString());

                Logger.Write(oe);
            }
            #endregion
        }

        /// <summary>
        /// Updates the parameters provided by overiding for partner.
        /// Specifically, this is added for Bing who provide a request with only from and 
        /// to location text, therefore programatically set their default values
        /// </summary>
        private void UpdateQueryParametersForPartner()
        {
            if (!string.IsNullOrEmpty(partnerId))
            {
                // Keys for partners
                string partnerPropertyKeyBase = "LandingPage.IFrame.JourneyLandingPage.PartnerId.";
                string partnerNameBing = "Bing";

                string partnerIdBing = Properties.Current[partnerPropertyKeyBase + partnerNameBing];

                // Update request for the Bing partner
                if (this.partnerId.ToLower().Trim().Equals(partnerIdBing.ToLower().Trim()))
                {
                    // Bing only want PT transport modes
                    // Check specified flag first to allow partner to control if they change their requirements
                    checkBoxPublicTransport = (ptModeSpecified) ? checkBoxPublicTransport : true;
                    checkBoxCarRoute = (carModeSpecified) ? checkBoxCarRoute : false;
                    showAdvanced = (showAdvancedSpecified) ? showAdvanced : false;
                    autoPlan = (autoPlanSpecified) ? autoPlan : true;
                }
            }
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

            // Because not using the SessionKey.LandingPageCheck key in session (as we don't want Door to door
            // to do landing page processing (its all done in this class)), manually set parameters to null and then
            // initialise
            sessionManager.JourneyParameters = null;
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

			// Set up origin Location and Search Type values
			originLocation.SearchType = GetSearchType(fromGazSpecified, fromDropDownLocationGazeteerOptions, textBoxFromText);
            originSearch.InputText = textBoxFromText;
			originSearch.SearchType = originLocation.SearchType;
			// Save them to the JourneyParameters
			journeyParameters.OriginLocation.SearchType = originLocation.SearchType;
			journeyParameters.Origin.SearchType = originLocation.SearchType;
			journeyParameters.Origin.InputText = originSearch.InputText;

            			
			// Set up destination Location and Search Type values
			destinationLocation.SearchType = GetSearchType(toGazSpecified, toDropDownLocationGazeteerOptions, textBoxToText);
			destinationSearch.InputText = textBoxToText;
			destinationSearch.SearchType = destinationLocation.SearchType;
			// Save them to the JourneyParameters
			journeyParameters.DestinationLocation.SearchType = destinationLocation.SearchType;
			journeyParameters.Destination.SearchType = destinationLocation.SearchType;
			journeyParameters.Destination.InputText = destinationSearch.InputText;

            // Save date control values to the JourneyParameters
			if(!(day == ""))
			{
				journeyParameters.OutwardDayOfMonth = day;
			}
			else
			{
				journeyParameters.OutwardDayOfMonth = Convert.ToString(DateTime.Today.Day);
			}
			if(!(monthYear == ""))
			{
                string[] monthYearArr = monthYear.Split(new char[] { '/' });
                
                // Code added due to existing bug in javascript for google desktop gadget.
                // month was coming as 010, 011, 012 when journey planned in october
                // So trim the code if it have 3 digits
                try
                {
                    if (monthYearArr.Length > 1)
                    {
                        string month = monthYearArr[0];
                        string year = monthYearArr[1];

                        if (!string.IsNullOrEmpty(month) && month.Length == 3)
                        {
                            month = month.Remove(0, 1);
                            monthYear = month + "/" + year;
                        }
                    }
                }
                finally
                {
                    journeyParameters.OutwardMonthYear = monthYear;
                }
			}
			else
			{
				journeyParameters.OutwardMonthYear = GetDefaultMonthYear();
			}

            // Initialise the time
            journeyParameters.InitialiseDefaultOutwardTime();

            // Update time to that in the request
			if(!string.IsNullOrEmpty(hour))
			{
				journeyParameters.OutwardHour = hour;
			}
			
			if(!string.IsNullOrEmpty(minute))
			{
				journeyParameters.OutwardMinute = minute;
			}
			
			//Set the fuel cost-consumption
			JourneyPlannerInputAdapter.SetFuelCostConsumption(journeyParameters);

			// Save the Find Public Transport / Car Journeys check box to Journey Parameters
			journeyParameters.PublicModes = new ModeType[]{};
			journeyParameters.PublicRequired = checkBoxPublicTransport;
			journeyParameters.PrivateRequired = checkBoxCarRoute;

			// Set all Modes of transport depending on wether or not PublicTransport is required.
			this.PublicRequired = checkBoxPublicTransport;
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

        /// <summary>
        /// Sets session info to view advanced options on JourneyPlannerInput page
        /// </summary>
        private void SetAdvancedSessionInfo(bool visible)
        {
            // Ensure full set of advanced options are visible
            //		View Public Transport Options
            sessionManager.InputPageState.PublicTransportOptionsVisible = visible;
            sessionManager.InputPageState.PublicTransportTypesVisible = visible;
            //		View Car Options
            sessionManager.InputPageState.CarOptionsVisible = visible;
            //		Set advanced options visible
            sessionManager.InputPageState.AdvancedOptionsVisible = visible;
        }

        /// <summary>
        /// Performs transition to JourneyPlannerInput page with advanced options selected
        /// </summary>
        private void PerformTransitionToJourneyPlannerAdvanced()
        {
            // Transfer to JourneyPlannerInput with advanced options
            sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.PlanAJourneyAdvanced;
        }

        #region Landing Page Request Parameters

        /// <summary>
		/// Determines the search type selected in the Gazateer. If the gazSpecified value is false or 
        /// the drop down gaz value is an empty string, then the location text is used to determine the 
        /// SearchType to return (either pattern matching for a postcode, or defaulting to a station/airport)
		/// </summary>
        /// <param name="gazSpecified">flag indicating if the DropDownGaz value was specified</param>
        /// <param name="dropDownGazateer">drop down gazetteer value to parse into a search type</param>
        /// <param name="locationText">location text used to determine SearchType if gazSpecified is false</param>
		private SearchType GetSearchType(bool gazSpecified, string dropDownGazateer, string locationText)
		{
			// Determine selected option from available Gazateer options
			SearchType returnSearchType;
            FindLocationGazeteerOptions findLocationGazeteerOptions;

            if (gazSpecified)
            {
                findLocationGazeteerOptions = GetFindLocationGazeteerOptions(dropDownGazateer);
            }
            else
            {
                // Use the locationText to determine SearchType

                // Is it a postcode
                if (PostcodeSyntaxChecker.IsPostCode(locationText) ||
                    PostcodeSyntaxChecker.IsPartPostCode(locationText))
                {
                    findLocationGazeteerOptions = FindLocationGazeteerOptions.AddressPostcode;
                }
                else
                {
                    // Default to station/airport
                    findLocationGazeteerOptions = FindLocationGazeteerOptions.StationAirport;
                }
            }
			
			// Set return search type according to the selected Gazateer option
			switch(findLocationGazeteerOptions)
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
			if (!(listValue ==""))
			{
				return (FindLocationGazeteerOptions) Enum.Parse(typeof(FindLocationGazeteerOptions), listValue);  
			}
				// hardcoding for default value to be address postcode
			else
			{
				return (FindLocationGazeteerOptions) Enum.Parse(typeof(FindLocationGazeteerOptions), "AddressPostcode"); 
			}
		}

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
		/// Method returns the current default month year as a string
		/// </summary>
		/// <param name="e"></param>
		private string GetDefaultMonthYear()
		{
			string month, year, monthYear;
			month = DateTime.Now.Month.ToString(CultureInfo.InvariantCulture);
			year = DateTime.Now.Year.ToString(CultureInfo.InvariantCulture);

			if (month.Length < 2)
			{
				month = "0" + month;
			}

			monthYear = month + "/" + year;		

			return monthYear;
		}

		/// <summary>
		/// Parses the query string variables and return param value
		/// </summary>
		/// <param name="paramName">Query string Parameter to be fetched</param>
		/// <returns></returns>
		private string GetValidParam(string paramName)
		{
			string tempCache = string.Empty;
			switch (paramName)
			{
					//Protect against XSS attacks (cross site scripting) by HTML Encoding the text.
				case paramFromGaz:
				case paramFromText:
				case paramToGaz:
				case paramToText:
				case paramMonthYear:
                case paramAdvanced:
				case paramPartnerId:
					tempCache = Server.UrlDecode(Page.Request.Params.Get(paramName));
					break;
				case paramDay:
				case paramHour:
				case paramMinute:
					try
					{
						int value = Convert.ToInt32(HttpUtility.UrlDecode(Page.Request.Params.Get(paramName)));
						tempCache = Server.UrlDecode(Page.Request.Params.Get(paramName));
					}
					catch (FormatException)
					{
						return string.Empty;
					}
					break;
				default:
					tempCache = string.Empty;
					break;
			}

			if (tempCache!= null)
			{
				return Server.HtmlEncode(tempCache);
			}
			else
			{
				return string.Empty;
			}
		}

		/// <summary>
		/// Find Boolean result according to input recieved from the query sting.
		/// Provides the default boolean value if the value is not specified. 
		/// </summary>
		/// <param name="paramName"></param>
		/// <returns>Boolean lookup of the parameter</returns>
        private bool GetBooleanSingleParam(string paramName)
        {
            switch (paramName)
            {
                case paramPublicTransport:
                case paramCar:
                case paramAutoPlan:
                    string strBoolTrueDefault = Page.Request.Params.Get(paramName);
                    if (strBoolTrueDefault != null && strBoolTrueDefault.Length != 0)
                    {
                        strBoolTrueDefault = strBoolTrueDefault.ToLower(CultureInfo.InvariantCulture);
                    }
                    switch (strBoolTrueDefault)
                    {
                        case "true":
                            return true;
                        case "false":
                            return false;
                        default:
                            return true;
                    }
                case paramAdvanced:
                    string strBoolFalseDefault = Page.Request.Params.Get(paramName);
                    if (!string.IsNullOrEmpty(strBoolFalseDefault))
                    {
                        strBoolFalseDefault = strBoolFalseDefault.ToLower(CultureInfo.InvariantCulture);
                    }

                    switch (strBoolFalseDefault)
                    {
                        case "true":
                            return true;
                        case "false":
                            return false;
                        default:
                            return false;
                    }
                default:
                    return true;
            }

        }

        /// <summary>
        /// Returns false if the parameter specified does not exist in the request query string
        /// </summary>
        /// <returns></returns>
        private bool DoesParamExist(string paramName)
        {
            string tempCache = Server.UrlDecode(Page.Request.Params.Get(paramName));

            if (tempCache == null)
                return false;
            else
                return true;
        }

        #endregion

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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion
	}
}
