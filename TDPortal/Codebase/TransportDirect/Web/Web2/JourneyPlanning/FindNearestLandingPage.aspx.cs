// *********************************************** 
// NAME                 : FindNearestLandingPage.aspx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/11/2007
// DESCRIPTION			: Find nearest landing page to handle find nearest Car Parks requests from third parties
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/FindNearestLandingPage.aspx.cs-arc  $ 
//
//   Rev 1.4   Mar 22 2013 10:49:14   dlane
//CCN0648c - Super Batch enhancements
//Resolution for 5907: CCN0648c - "Super Batch" enhancements
//
//   Rev 1.3   Oct 01 2009 10:54:18   pghumra
//Applied changes for cycle planner landing page, latitude longitude coordinates in landing page and find nearest car park functionality
//Resolution for 5316: CCN537 Cycle Planning Page Landing
//Resolution for 5317: CCNxxx Lat Long Coordinates in Page Landing
//
//   Rev 1.2   Mar 31 2008 13:24:36   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 29 2007 11:00:38   mturner
//Initial revision.
//
//   Rev 1.1   Nov 08 2007 17:08:00   mmodi
//Amended to accept encrypted parameter
//Resolution for 4524: Del 9.8 - Car Parking Page Landing
//
//   Rev 1.0   Nov 08 2007 14:11:20   mmodi
//Initial revision.
//Resolution for 4524: Del 9.8 - Car Parking Page Landing
//

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
using TransportDirect.CommonWeb.Helpers;

using Logger = System.Diagnostics.Trace;
using LocationSelectControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType;
using ControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.ControlType;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Web.Adapters;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for FindNearestLandingPage.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class FindNearestLandingPage : TDPage
	{
		#region Declarations
		#region Controls
		protected TransportDirect.UserPortal.Web.Controls.HeadElementControl headElementControl;

		// Controls used in setting up Find nearest parameters
		protected TransportDirect.UserPortal.Web.Controls.FindToFromLocationsControl findAOriginLocationsControl;
		#endregion

		#region Private members
		// Default page on error
		PageId defaultPage = PageId.FindCarParkInput;

		// To ensure all parameters are valid
		bool validParameters = true;

		// Car park specific 
		SearchType searchType;
		#endregion

		#region Search parameters
		/// <summary>
		/// Partner Id (shortcut "id")
		/// </summary>
		private string partnerId = String.Empty;
		/// <summary>
		/// Entry type (shortcut "et")
		/// </summary>
		private string entrytype = String.Empty;
		/// <summary>
		/// Find nearest type (shortcut "ft")
		/// </summary>
		private string findnearesttype = String.Empty;
		/// <summary>
		/// Place text (shortcut "pn")
		/// </summary>
		private string place = String.Empty;
		/// <summary>
		/// Location gazetteer (shortcut "lg") 
		/// e.g. "AddressPostcode"
		/// </summary>
		private string locationGazetteer = String.Empty;
		/// <summary>
		/// Number displayed (shortcut "nd")
		/// </summary>
		private string numberdisplayed = String.Empty;
		/// <summary>
		/// Auto plan flag (shortcut "p")
		/// </summary>
		private bool autoplanflag = false;

        /// <summary>
        /// Location type (shotcut: "lo")
        /// e.g. "p" (placename)
        /// </summary>
        private string locationtype = String.Empty;

        /// <summary>
        /// Location data
        /// </summary>
        private string locationdata = String.Empty;

        TDLocation location;
        LocationSearch search;
        LocationSelectControlType originLocationSelectControlType;
                

		#endregion

		#region Global definitions
		private static readonly char[] DELIMITER = new char[] {','};
		private static readonly char[] SECTION_SEP = new char[] { '&' };
		private static readonly char[] SUBSECTION_SEP = new char[] { ':' };
		private const int ID_SECTION = 0;
		private const int PLACE_SECTION = 1;
		private const int LOCATIONGAZETTEER_SECTION = 2;
        private const int LOCATIONTYPE_SECTION = 3;
        private const int LOCATIONDATA_SECTION = 4;

		private const int ID_KEY = 0;
		private const int ID_VAL = 1;

		private const int PLACE_KEY = 0; 
		private const int PLACE_VAL = 1;

		private const int LOCATIONGAZETTEER_KEY = 0; 
		private const int LOCATIONGAZETTEER_VAL = 1;

        private const int LOCATIONTYPE_KEY = 0;
        private const int LOCATIONTYPE_VAL = 1;

        private const int LOCATIONDATA_KEY = 0;
        private const int LOCATIONDATA_VAL = 1;

		private const string HTTP_REQUEST_TYPE_GET  = "GET";
		private const string HTTP_REQUEST_ID_STRING1 = "?id=";
		private const string HTTP_REQUEST_ID_STRING2 = "&id=";
		private const char AMPERSAND = '&';

		private const string PREFIX_HTTP  = "http";
		private const string PREFIX_HTTPS = "https";
		private const string PREFIX_WWW	  = "www";

		private const int SAFE_PARTNERID_LENGTH = 10;

		private const string AUTOPLANOFF = "off";

		//use flags to detect if origin or destination is being processed
		private const string ORIGIN_FLAG = "origin";
		private const string DEST_FLAG = "destination";

        //origin destination flag
        private string odFlag = string.Empty;

		//final url that will do the redirect
		private string complete_url = String.Empty;
			
		#endregion
		#endregion
		
		#region Constructor
		/// <summary>
		/// Page constructor
		/// </summary>
		public FindNearestLandingPage() : base()
		{
			pageId = PageId.FindNearestLandingPage;
            location = new TDLocation();
            search = new LocationSearch();
            originLocationSelectControlType = new LocationSelectControlType(ControlType.Default);
		}
		#endregion

		#region Page Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			#region Create session
			// create new session and itinerary managers
			ITDSessionManager sessionManager = TDSessionManager.Current;
			TDItineraryManager itineraryManager = TDItineraryManager.Current;
			// Reset the itinerary manager
			itineraryManager.NewSearch();

			#endregion

			#region Set parameters required in session
			// need to set the landingpage switch to true to identify the request as a Landing page request
			// this needs to be done before the search parameters are initialised since there are 
			// dependencies upon it
			TDSessionManager.Current.Session[ SessionKey.LandingPageCheck ] = true;

			//place the autoplan switch into the session object
			autoplanflag = GetBooleanSingleParam("p");

			TDSessionManager.Current.Session[ SessionKey.LandingPageAutoPlan ] = autoplanflag;
			
			#endregion

			#region Set redirect URL
			// Determine which page we need to redirect to
			entrytype = GetValidSingleParam("et");
			findnearesttype = GetValidSingleParam("ft");
			if (TDPage.SessionChannelName !=  null )
			{
				// Add system time to end of redirect URL (fake querystring param). This ensures IE always requests latest version of page rather than relying on cache. IR3360.
				complete_url = GetRedirectUrl(entrytype, findnearesttype) + "?x=" + Server.UrlEncode(DateTime.UtcNow.ToLongTimeString()) + "&SID=" + TDSessionManager.Current.Session.SessionID + "&IsFNLanding=true";
			}
			#endregion

			#region Call method to set up planner specific parameters

			SetCommonParameters();

			// determine what type of search is required and Set Parameters accordingly
			switch (findnearesttype)
			{	
				case "cp":
					SetFindACarParkParameters();					
					break;
				default:
					validParameters = false;
					
					// Log an error and do nothing, it'll redirect to the default page set in GetRedirectUrl()
					string message = "FindNearestLandingPage request was abandoned due to one or more invalid parameters in requeststring: "
						+ HttpUtility.UrlDecode(Page.Request.RawUrl);
					LogError(message, partnerId);

					break;
			}
			#endregion

			if (!validParameters)
				// Reset the autoplan to prevent planner from submitting invalid request
				TDSessionManager.Current.Session[ SessionKey.LandingPageAutoPlan ] = false;

			Response.Redirect(complete_url);
		}
		#endregion

        private void SetPlacenameLocationParameters(string searchData)
        {
            location = new TDLocation();
            search = new LocationSearch();
            search.FuzzySearch = false;
            search.VagueSearch = false;
            search.InputText = searchData;
            search.SearchType = searchType;

            findAOriginLocationsControl.OriginLocation = location;
            findAOriginLocationsControl.OriginSearch = search;
            findAOriginLocationsControl.OriginControlType = new TDJourneyParameters.LocationSelectControlType(ControlType.Default);
            findAOriginLocationsControl.RefreshControl();
            findAOriginLocationsControl.Search();
        }

        /// <summary>
        /// Populate locality data into relevant object hierarchy
        /// </summary>
        /// <param name="osgr"></param>
        /// <returns></returns>
        private string PopulateLocality(OSGridReference osgr)
        {
            IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

            return gisQuery.FindNearestLocality(osgr.Easting, osgr.Northing);
        }

        private void ResetSingleLocation(TDLocation location, LocationSearch search, TDJourneyParameters parameters)
        {
            location.Status = TDLocationStatus.Unspecified;
            search.LocationFixed = false;
            location.ClearAll();
            search.ClearAll();
            if (odFlag == ORIGIN_FLAG)
            {
                parameters.OriginType = new LocationSelectControlType(ControlType.Default);
            }
            else if (odFlag == DEST_FLAG)
            {
                parameters.DestinationType = new LocationSelectControlType(ControlType.Default);
            }
        }

        private void SetOsGridLocationParamaters(string searchData)
        {
            try
            {
                location.Description = place;
                search.InputText = searchData;
                string strdefaultOsgren = searchData;
                string[] defaultgrids = strdefaultOsgren.Split(DELIMITER);
                if (defaultgrids != null && defaultgrids.Length == 2)
                {
                    int defaulteasting = int.Parse(defaultgrids[0], CultureInfo.InvariantCulture);
                    int defaultnorthing = int.Parse(defaultgrids[1], CultureInfo.InvariantCulture);
                    OSGridReference defaultosgren = new OSGridReference(defaulteasting, defaultnorthing);
                    location.GridReference = defaultosgren;
                    location.PopulateToids();
                    location.Locality = PopulateLocality(defaultosgren);
                    location.RequestPlaceType = RequestPlaceType.Coordinate;
                    location.Status = TDLocationStatus.Valid;
                    search.LocationFixed = true;
                }
                else
                {
                    // OSGR is default location type. If location data is not two comma seperated values
                    // then not valid coordinates, so have to ignore values. 
                    ResetSingleLocation(location, search, TDSessionManager.Current.JourneyParameters);
                }
            }
            catch (FormatException)
            {
                //osgrid not resolved, redirect to input page
                OperationalEvent defaultoe =
                    new OperationalEvent(TDEventCategory.Business,
                    TDTraceLevel.Error,
                    "OSgrid Provided could not be resolved." +
                    " Partner ID : " + partnerId + " client-ip : "
                    + Page.Request.UserHostAddress.ToString() + " url-referrer : "
                    + Page.Request.UrlReferrer);
                Logger.Write(defaultoe);

                throw;
            }
        }

        private void SetLatLongLocationParamaters(string searchData)
        {
            try
            {
                location.Description = place;
                search.InputText = searchData;
                string strdefaultOsgren = searchData;
                string[] defaultgrids = strdefaultOsgren.Split(DELIMITER);
                if (defaultgrids != null && defaultgrids.Length == 2)
                {
                    double defaultlatitude = double.Parse(defaultgrids[0], CultureInfo.InvariantCulture);
                    double defaultlongitude = double.Parse(defaultgrids[1], CultureInfo.InvariantCulture);
                    LatitudeLongitude defaultlatlong = new LatitudeLongitude(defaultlatitude, defaultlongitude);
                    LandingPageHelper lphelper = new LandingPageHelper();

                    OSGridReference osgr = lphelper.GetOSGRFromLatLong(defaultlatlong);
                    locationdata = osgr.Easting.ToString() + ", " + osgr.Northing.ToString();
                    search.InputText = locationdata;
                    SetOsGridLocationParamaters(locationdata);
                }
                else
                {
                    // OSGR is default location type. If location data is not two comma seperated values
                    // then not valid coordinates, so have to ignore values. 
                    ResetSingleLocation(location, search, TDSessionManager.Current.JourneyParameters);
                }
            }
            catch (FormatException)
            {
                //osgrid not resolved, redirect to input page
                OperationalEvent defaultoe =
                    new OperationalEvent(TDEventCategory.Business,
                    TDTraceLevel.Error,
                    "LatitudeLongitude Provided could not be resolved." +
                    " Partner ID : " + partnerId + " client-ip : "
                    + Page.Request.UserHostAddress.ToString() + " url-referrer : "
                    + Page.Request.UrlReferrer);
                Logger.Write(defaultoe);

                throw;
            }
        }


		#region Find Nearest set up methods
		/// <summary>
		/// Method to set parameters required on the Find A Car Park input page
		/// </summary>
		private void SetFindACarParkParameters()
		{
			// Get the parmeters we need from the query string
			place = GetValidSingleParam("pn");
			locationGazetteer = GetValidSingleParam("lg");			
			numberdisplayed	= GetValidSingleParam("nd");
            locationtype = GetValidSingleParam("lo");
            locationdata = GetValidSingleParam("l");

			// Check if there are any encrypted parameters and extract.
			// if there are, then expecting place and location gaz to be encrypted
			string encryptedParam = GetValidSingleParam("enc");
		
			if (encryptedParam.Length > 0)
			{
				string[] decodedInput = DecryptedData(encryptedParam);
				GetEncryptedFindACarParkParameters(decodedInput);
			}

			// Set the searchtype
			searchType = GetSearchType(locationGazetteer);

			// Clear and intialise session ready for Car Park page landing
			TDSessionManager.Current.IsFromNearestCarParks = true;
			TDSessionManager.Current.FindPageState = FindPageState.CreateInstance(FindAMode.CarPark);
			TDSessionManager.Current.FindCarParkPageState.Initialise();
			TDSessionManager.Current.JourneyParameters = new TDJourneyParametersMulti();
			TDSessionManager.Current.JourneyParameters.Initialise();
			TDSessionManager.Current.InitialiseJourneyParameters(FindAMode.CarPark);

			if (validParameters)
			{				
				// SetupCarParkPlannerSearch
				FindCarParkPageState pageState = new FindCarParkPageState();
                SetLocationParameters();
                findAOriginLocationsControl.FromLocationControl.TriLocationControl.Populate(DataServiceType.FindCarLocationDrop, CurrentLocationType.From, ref search, ref location, ref originLocationSelectControlType, false, false, true, false);
                pageState.CurrentLocation = location;
                pageState.CurrentSearch = search;
                pageState.SearchFrom = search;
				pageState.SearchFrom.FuzzySearch = false;
				pageState.SearchFrom.VagueSearch = false;
                if (locationtype == "p")
                {
                    if (string.IsNullOrEmpty(locationdata))
                    {
                        pageState.SearchFrom.InputText = place;
                    }
                    else
                    {
                        pageState.SearchFrom.InputText = locationdata;
                    }
                }
                else
                {
                    pageState.SearchFrom.InputText = locationdata;
                }
				pageState.SearchFrom.SearchType = searchType;
				pageState.MaxNumberOfCarParks = (numberdisplayed != string.Empty) ? Convert.ToInt32(numberdisplayed) : -1;
                
				TDSessionManager.Current.FindCarParkPageState = pageState;
				TDSessionManager.Current.FindCarParkPageState.CurrentSearch =  pageState.SearchFrom;
			}
			else
			{
				string message = "FindNearestLandingPage request was abandoned due to one or more invalid parameters in requeststring: "
					+ HttpUtility.UrlDecode(Page.Request.RawUrl);
				LogError(message, partnerId);
			}

			//MIS logging				
			LandingPageEntryEvent lpee = new LandingPageEntryEvent(partnerId, LandingPageService.FindACarPark, Session.SessionID, TDSessionManager.Current.Authenticated);
			Logger.Write(lpee);
		}

		#endregion

		#region Private methods

		#region Private methods - Get redirect, Set common

        private void SetLocationParameters()
        {
            string searchData = string.Empty;
            if (string.IsNullOrEmpty(locationtype))
            {
                locationtype = "p";
            }
            if (locationtype == "p")
            {
                if (string.IsNullOrEmpty(locationdata))
                {
                    searchData = place;
                }
                else
                {
                    searchData = locationdata;
                }
            }
            else
            {
                searchData = locationdata;
            }
            if ((!string.IsNullOrEmpty(locationdata)) && (!string.IsNullOrEmpty(place)) && (!string.IsNullOrEmpty(searchData)))
            {
                try
                {
                    switch (locationtype)
                    {
                        case "p":
                            SetPlacenameLocationParameters(searchData);
                            break;
                        case "en":
                            SetOsGridLocationParamaters(searchData);
                            break; 
                        case "l":
                            SetLatLongLocationParamaters(searchData);
                            break;
                        default:
                            SetPlacenameLocationParameters(searchData);
                            break;
                    }
                }
                catch (FormatException)
                {
                    ResetSingleLocation(location, search, TDSessionManager.Current.JourneyParameters);
                }
            }
            else
            {
                ResetSingleLocation(location, search, TDSessionManager.Current.JourneyParameters);
            }
        }

		/// <summary>
		/// Uses the findnearesttype parameter to determine which page to redirect to.
		/// </summary>
		/// <param name="entrytype">Entry Type</param>
		/// <param name="mode">FindNearestType</param>
		/// <returns>Full URL for redirection</returns>
		private string GetRedirectUrl (string entrytype, string findnearesttype)
		{
			string pageURL = String.Empty;
			//create a page controller object in order to get the page transfer details for the 
			//required mode
			IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];
			PageTransferDetails pageTransferDetails;

			// Check if an entry type is provided, that its correct. 
			// Not providing one is ok, as we can assume requester wanted the findnearest as they used this landingpage url
			if ((entrytype == string.Empty) || (entrytype == "fn"))
			{
				switch (findnearesttype)
				{
					case "cp":
						pageTransferDetails = pageController.GetPageTransferDetails(PageId.FindCarParkInput);
						break;
					default: // send to default page
						pageTransferDetails = pageController.GetPageTransferDetails(defaultPage);
						validParameters = false;
						break;
				}
			}
			else
			{	// Incorrct entry type, so redirect to default page
				pageTransferDetails = pageController.GetPageTransferDetails(defaultPage);

				string message = "FindNearestLandingPage request was made with an incorrect parameter entrytype: " + entrytype 
					+ " . Expected entry type 'fn' ";
				LogError(message, partnerId);

				validParameters = false;
			}

			pageURL = pageTransferDetails.PageUrl;

			return getBaseChannelURL(TDPage.SessionChannelName) + pageURL;
		}
	
		/// <summary>
		/// Obtains partner id from request. If parameters are encrypted then performs decryption.
		/// </summary>
		private void SetCommonParameters() 
		{
			string encryptedParam = GetValidSingleParam("enc");
		
			if (encryptedParam.Length > 0)
			{
				string[] decodedInput = DecryptedData(encryptedParam);
				//Get and verify the encrypted partner id
				partnerId = CheckPartnerId(decodedInput);
			}
			else
			{
				//Get and verify the unencrypted partner id
				partnerId = CheckPartnerId(GetValidSingleParam("id"));
			}
		}
		#endregion

		#region Private methods - Search type and Location gazetteer
		/// <summary>
		/// Determines the search type from the loc gaz plain text.
		/// Defaults to AddressPostcode
		/// </summary>
		/// <returns></returns>
		private SearchType GetSearchType(string locationGazetteer)
		{
			try
			{
				SearchType returnSearchType;
				FindLocationGazeteerOptions findLocationGazeteerOptions = GetFindLocationGazetteerOptions(locationGazetteer);
			
				switch(findLocationGazeteerOptions)
				{
					case FindLocationGazeteerOptions.AttractionFacility: 
						returnSearchType = SearchType.POI; 
						break;
					case FindLocationGazeteerOptions.CityTownSuburb: 
						// Note: Search Type Locality = City/Town/Suburb - NOT City
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
			catch
			{
				// Any exceptions mean an invalid/unrecognised locationgazetteer was supplied
				// Therefore default it to Address.Postcode, and set valid params to false
				validParameters = false;
				return SearchType.AddressPostCode;
			}
		}

		/// <summary>
		///	 Returns the GazeteerOptions selected by the user 
		/// </summary>
		/// <param name="listValue">GazeteerOptions as string</param>
		/// <returns>FindLocationGazeteerOptions selected by the user</returns>
		private FindLocationGazeteerOptions  GetFindLocationGazetteerOptions(string listValue)
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

		#endregion

		#region Private methods - query string parameters
		/// <summary>
		/// Return parameters from querystring arguments.
		/// </summary>
		/// <param name="paramName"></param>
		/// <returns>String lookup of the parameter</returns>
		private string GetValidSingleParam (string paramName)
		{
			string tempCache = string.Empty;

			switch (paramName)
			{
					//Protect against XSS attacks (cross site scripting) by HTML Encoding the text.
					// Some of these parameter strings are displayed as is on portal, so must be made safe.				
				case "id":
				case "et":
				case "ft":
				case "pn":
				case "lg":
                case "lo":
                case "l":
					tempCache = HttpUtility.UrlDecode(Page.Request.Params.Get(paramName));
					break;
				case "enc":
					tempCache = Page.Request.Params.Get(paramName);
					break;
				case "nd":
					try
					{
						int value = Convert.ToInt32(HttpUtility.UrlDecode(Page.Request.Params.Get(paramName)));
						tempCache = HttpUtility.UrlDecode(Page.Request.Params.Get(paramName));
					}
					catch (FormatException)
					{
						validParameters = false;

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
		private bool GetBooleanSingleParam (string paramName)
		{
			switch (paramName)
			{	
					//auto plan flag
				case "p":
					if (Page.Request.Params.Get(paramName) == "1")
						return true;
					else
						return false;
				default:
					return false;
			}
		}

		#endregion

		#region Private methods - encryption/decription
		/// <summary>
		/// Decrypts encrypted data and parses into a string array
		/// </summary>
		/// <param name="encrypteddata">Encrypted data</param>
		/// <returns>String array of decrypted data</returns>
		private string[] DecryptedData(string encrypteddata)
		{
			string[] resultsCache;
			try
			{
				ITDCrypt decryptionEngine = (ITDCrypt)TDServiceDiscovery.Current[ ServiceDiscoveryKey.Crypto ];
				string decryptedText = decryptionEngine.AsymmetricDecrypt(encrypteddata);
				resultsCache = decryptedText.Split( SECTION_SEP );	
			}
			catch (System.ArgumentNullException)
			{
				//enc argument has not been found
				resultsCache = new string[0];
			}
			
			return resultsCache;
		}

		private void GetEncryptedFindACarParkParameters(string[] decodedInput)
		{
			// Ensure the size of the decoded input
			string[] place_section = decodedInput[PLACE_SECTION].Split( SUBSECTION_SEP );
			
			// Verify/fail if place_section length != 2 and that part place_section[ID_KEY] == "pn"
			if ((place_section.Length != 2) || (place_section[PLACE_KEY].CompareTo("pn") != 0))
			{
				string message = "FindNearestLandingPage CarPark encrypted parameter PlaceName provided does not conform to the pn=[PlaceName] format.";
				
				LogError(message, partnerId);
				
				//Redirect to the default page set in GetRedirectUrl()
				Response.Redirect(complete_url);
			}
			else
			{
				place = place_section[PLACE_VAL];
			}

			string[] locationGaz_section = decodedInput[LOCATIONGAZETTEER_SECTION].Split( SUBSECTION_SEP );

			if ((locationGaz_section.Length != 2) || (locationGaz_section[LOCATIONGAZETTEER_KEY].CompareTo("lg") != 0))
			{
				string message = "FindNearestLandingPage CarPark encrypted parameter LocationGazetteer provided does not conform to the lg=[LocationGazetter] format.";
				
				LogError(message, partnerId);
				
				//Redirect to the default page set in GetRedirectUrl()
				Response.Redirect(complete_url);
			}
			else
			{
				locationGazetteer = locationGaz_section[LOCATIONGAZETTEER_VAL];
			}


            string[] locationType_section = decodedInput[LOCATIONTYPE_SECTION].Split(SUBSECTION_SEP);

            if ((locationType_section.Length != 2) || (locationType_section[LOCATIONTYPE_KEY].CompareTo("lo") != 0))
            {
                string message = "FindNearestLandingPage CarPark encrypted parameter LocationType provided does not conform to the lo=[LocationType] format.";

                LogError(message, partnerId);

                //Redirect to the default page set in GetRedirectUrl()
                Response.Redirect(complete_url);
            }
            else
            {
                locationtype = locationType_section[LOCATIONTYPE_VAL];
            }


            string[] locationData_section = decodedInput[LOCATIONDATA_SECTION].Split(SUBSECTION_SEP);

            if ((locationData_section.Length != 2) || (locationData_section[LOCATIONDATA_KEY].CompareTo("l") != 0))
            {
                string message = "FindNearestLandingPage CarPark encrypted parameter LocationData provided does not conform to the l=[LocationData] format.";

                LogError(message, partnerId);

                //Redirect to the default page set in GetRedirectUrl()
                Response.Redirect(complete_url);
            }
            else
            {
                locationdata = locationData_section[LOCATIONDATA_VAL];
            } 
		}

		#endregion

		#region Private methods - Partner id
		/// <summary>
		/// Check that the partner Id is valid
		/// </summary>
		/// <param name="partnerId">string representation of Partner ID</param>
		/// <returns>string Partner Id</returns>
		private string CheckPartnerId(string partnerId)
		{
			Logger.Write(new OperationalEvent(TDEventCategory.Business,
				TDTraceLevel.Verbose, "RawUrl (decoded) = " + HttpUtility.UrlDecode(Page.Request.RawUrl)));

			if	(Page.Request.RequestType == HTTP_REQUEST_TYPE_GET)
			{
				string urlText = HttpUtility.UrlDecode(Page.Request.RawUrl);

				int startIndex	= urlText.IndexOf(HTTP_REQUEST_ID_STRING1); 

				if	(startIndex > 0) 
				{
					startIndex += HTTP_REQUEST_ID_STRING1.Length;
				}
				else 
				{
					startIndex	= urlText.IndexOf(HTTP_REQUEST_ID_STRING2);

					if	(startIndex > 0) 
					{
						startIndex += HTTP_REQUEST_ID_STRING2.Length;
					}
				}

				if	(startIndex > 0)
				{
					int endIndex = urlText.IndexOf(AMPERSAND, startIndex);
				
					if	(endIndex > 0)
					{
						partnerId = urlText.Substring(startIndex, endIndex - startIndex); 
					}
					else
					{
						partnerId = urlText.Substring(startIndex);
					}
				}
			}

			Logger.Write(new OperationalEvent(TDEventCategory.Business,
				TDTraceLevel.Verbose, "PartnerId = " + partnerId));

			// replace invalid characters with empty strings, and limit to ten characters.
			string safePartnerId = System.Text.RegularExpressions.Regex.Replace(partnerId, @"[^\w]", "");

			// before truncating to ten chars, strip off any leading "http" or "https" or "www",
			//  since they waste valuable characters and don't add to identifying the client ...

			if	(safePartnerId.StartsWith(PREFIX_HTTPS))
			{
				safePartnerId = safePartnerId.Substring(PREFIX_HTTPS.Length);
			}
			else if	(safePartnerId.StartsWith(PREFIX_HTTP))
			{
				safePartnerId = safePartnerId.Substring(PREFIX_HTTP.Length);
			}

			if	(safePartnerId.StartsWith(PREFIX_WWW))
			{
				safePartnerId = safePartnerId.Substring(PREFIX_WWW.Length);
			}
 
			if (safePartnerId.Length > SAFE_PARTNERID_LENGTH)
			{
				safePartnerId = safePartnerId.Substring(0, SAFE_PARTNERID_LENGTH);
			}

			Logger.Write(new OperationalEvent(TDEventCategory.Business,
				TDTraceLevel.Verbose, "safePartnerId = " + safePartnerId));

			return safePartnerId;
		}

		/// <summary>
		/// Check that the encrypted partner Id is valid
		/// </summary>
		/// <param name="decodedInput">Decoded input data</param>
		/// <returns>string Partner Id</returns>
		private string CheckPartnerId(string[] decodedInput)
		{
			// Probably ensure the size of the decoded input
			string[] vendor_section = decodedInput[ID_SECTION].Split( SUBSECTION_SEP );
			// Verify/fail if vendor_section length != 2 and that part vendor_section[ID_KEY] == "id"
			// Does not conform to 
			// id=[vendorid]
			if ((vendor_section.Length != 2) || (vendor_section[ID_KEY].CompareTo("id") != 0))
			{
				string message = "Partner section provided does not conform to the id=[partnerID] " + "format.";
				
				LogError(message, "[partnerID]");
				
				//Redirect to the default page set in GetRedirectUrl()
				Response.Redirect(complete_url);
			}

			return CheckPartnerId(vendor_section[ID_VAL]);
			
		}
		#endregion

		#endregion

		#region Error logging
		/// <summary>
		/// Write operational events to the log, logging ip address and Partner ID
		/// </summary>
		/// <param name="description">The description of the error</param>
		/// <param name="partnerId">The error id</param>
		private void LogError (string description, string partnerId)
		{
			StringBuilder message = new StringBuilder(string.Empty);
			message.Append(description);
			message.Append(" Partner ID : ");
			message.Append(partnerId);
			message.Append(" client-ip : ");
			message.Append(Page.Request.UserHostAddress.ToString(CultureInfo.InvariantCulture));
			message.Append(" url-referrer : ");
			message.Append(Page.Request.UrlReferrer);

			OperationalEvent oe = new  OperationalEvent(
				TDEventCategory.Business, TDTraceLevel.Error, message.ToString());

			Logger.Write(oe);
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
