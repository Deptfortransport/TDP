// *********************************************** 
// NAME                 : StopInformationLandingPage.aspx.cs
// AUTHOR               : Amit Patel
// DATE CREATED         : 10/09/2009 
// DESCRIPTION  		: Stop information landing page CCN 526
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/StopInformationLandingPage.aspx.cs-arc  $
//
//   Rev 1.6   Oct 29 2010 11:21:42   RBroddle
//Removed explicit wire up to Page_Load as AutoEventWireUp=true for this page so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.5   Nov 17 2009 18:03:18   mmodi
//Update default page to new map page
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.4   Oct 15 2009 14:49:10   apatel
//Stop Information Departure Board Service code changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.3   Oct 06 2009 14:41:44   apatel
//Stop Information code changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.2   Sep 15 2009 15:46:02   apatel
//Updated to set show plan a journey control flag
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.1   Sep 14 2009 15:19:20   apatel
//Stop Information Pages - CCN 526
//Resolution for 5315: CCN526 Stop Information Page Landing

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportDirect.Common;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using Logger = System.Diagnostics.Trace;
using TransportDirect.UserPortal.ScreenFlow;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.Logging;
using System.Text;
using System.Globalization;
using TransportDirect.UserPortal.LocationService;

using ControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.ControlType;
using LocationSelectControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType;

namespace TransportDirect.UserPortal.Web.JourneyPlanning
{
    /// <summary>
    /// Summary description for StopInformationLandingPage.
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    public partial class StopInformationLandingPage : TDPage
    {

        // Default page on error
        PageId defaultPage = PageId.FindMapInput;


        // To ensure all parameters are valid
        bool validParameters = true;

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
        /// Stop data type (shortcut "st")
        /// </summary>
        private string stopDataType = String.Empty;
        /// <summary>
        /// Stop data (shortcut "sd") 
        /// </summary>
        private string stopData = String.Empty;
        /// <summary>
        /// Excluded functions (shortcut "ef")
        /// </summary>
        private string excludedFunctions = String.Empty;
        

        #endregion

        #region Global definitions
        private static readonly char[] DELIMITER = new char[] { ',' };
        private static readonly char[] SECTION_SEP = new char[] { '&' };
        private static readonly char[] SUBSECTION_SEP = new char[] { ':' };
        private const int ID_SECTION = 0;
        private const int STOPTYPE_SECTION = 1;
        private const int STOPDATA_SECTION = 2;

        private const int ID_KEY = 0;
        private const int ID_VAL = 1;

        private const int STOPTYPE_KEY = 0;
        private const int STOPTYPE_VAL = 1;

        private const int STOPDATA_KEY = 0;
        private const int STOPDATA_VAL = 1;

        private const string HTTP_REQUEST_TYPE_GET = "GET";
        private const string HTTP_REQUEST_ID_STRING1 = "?id=";
        private const string HTTP_REQUEST_ID_STRING2 = "&id=";
        private const char AMPERSAND = '&';

        private const string PREFIX_HTTP = "http";
        private const string PREFIX_HTTPS = "https";
        private const string PREFIX_WWW = "www";

        private const int SAFE_PARTNERID_LENGTH = 10;

        private const string AUTOPLANOFF = "off";

        //use flags to detect if origin or destination is being processed
        private const string ORIGIN_FLAG = "origin";
        private const string DEST_FLAG = "destination";

        //final url that will do the redirect
        private string complete_url = String.Empty;

        #endregion

        #region Constructor
        /// <summary>
		/// Page constructor
		/// </summary>
        public StopInformationLandingPage() : base()
		{
			pageId = PageId.StopInformationLandingPage;

        }
        #endregion

        #region Page load event

        /// <summary>
        /// Page load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
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
            TDSessionManager.Current.Session[SessionKey.LandingPageCheck] = true;

            #endregion

            #region Set redirect URL
            // Determine which page we need to redirect to
            entrytype = GetValidSingleParam("et");
            
            if (TDPage.SessionChannelName != null)
            {
                // Add system time to end of redirect URL (fake querystring param). This ensures IE always requests latest version of page rather than relying on cache. IR3360.
                complete_url = GetRedirectUrl(entrytype) + "?x=" + Server.UrlEncode(DateTime.UtcNow.ToLongTimeString()) + "&SID=" + TDSessionManager.Current.Session.SessionID + "&IsSILanding=true";
            }
            #endregion

            #region Call method to set up planner specific parameters

            SetCommonParameters();

            SetStopInformationParameters();
                
            
            #endregion

            if (!validParameters)
            {
                ResetDefaultPageSessionValues();

                IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];
                PageTransferDetails pageTransferDetails;

                // Incorrct entry type, so redirect to default page
                pageTransferDetails = pageController.GetPageTransferDetails(defaultPage);

                complete_url = getBaseChannelURL(TDPage.SessionChannelName) + pageTransferDetails.PageUrl;

                // Reset the landing page flag as because want to display page in default state
                TDSessionManager.Current.Session[SessionKey.LandingPageCheck] = false;
            }

            Response.Redirect(complete_url);
        }

        #endregion

        #region Find Nearest set up methods
        /// <summary>
        /// Method to set parameters required on the Stop Information page
        /// </summary>
        private void SetStopInformationParameters()
        {
            // Get the parmeters we need from the query string
            stopDataType = GetValidSingleParam("st");
            stopData = GetValidSingleParam("sd");
            excludedFunctions = GetValidSingleParam("ef");

            // Check if there are any encrypted parameters and extract.
            // if there are, then expecting place and location gaz to be encrypted
            string encryptedParam = GetValidSingleParam("enc");

            if (encryptedParam.Length > 0)
            {
                string[] decodedInput = DecryptedData(encryptedParam);
                GetEncryptedStopInformationParameters(decodedInput);
            }

           
            // Clear and intialise session ready for Stop Information page landing
            TDSessionManager.Current.IsStopInformationMode = true;
            TDSessionManager.Current.JourneyParameters = new TDJourneyParametersMulti();
            TDSessionManager.Current.JourneyParameters.Initialise();
            TDSessionManager.Current.InputPageState.Initialise();

            // Check flag as it might have been set to false in GetRedirectURL()
            if (validParameters)
            {
                validParameters = StopCodeResolved(stopData);
            }

            if (validParameters)
            {
                //Set up stop information page configurationg info in input page state
                InputPageState inputPageState = TDSessionManager.Current.InputPageState;
                inputPageState.StopCode = stopData;
                inputPageState.StopCodeType = GetStopCodeDataType(stopDataType);
                inputPageState.ShowStopInformationPlanJourneyControl = true;

                string[] functionsToExclude = excludedFunctions.Split(DELIMITER);

                foreach (string ef in functionsToExclude)
                {
                    switch (ef.Trim().ToLower())
                    {
                        case "m":
                            inputPageState.ShowStopInformationMapControl = false;
                            break;
                        case "j":
                            inputPageState.ShowStopInformationPlanJourneyControl = false;
                            break;
                        case "t":
                            inputPageState.ShowStopInformationTaxiControl = false;
                            break;
                        case "o":
                            inputPageState.ShowStopInformationOperatorControl = false;
                            break;
                        case "n":
                            inputPageState.ShowStopInformationDepartureBoardControl = false;
                            break;
                        case "r":
                            inputPageState.ShowStopInformationRealTimeControl = false;
                            break;
                        case "l":
                            inputPageState.ShowStopInformationLocalityControl = false;
                            break;
                        case "f":
                            inputPageState.ShowStopInformationFacilityControl = false;
                            break;
 
                    }
                }
                
            }
            else
            {
                string message = "StopInformationLandingPage request was abandoned due to one or more invalid parameters in requeststring: "
                    + HttpUtility.UrlDecode(Page.Request.RawUrl);
                LogError(message, partnerId);

                validParameters = false;
            }

            //MIS logging				
            LandingPageEntryEvent lpee = new LandingPageEntryEvent(partnerId, LandingPageService.StopInformation, Session.SessionID, TDSessionManager.Current.Authenticated);
            Logger.Write(lpee);
        }

        

        #endregion

        #region Private methods

        #region Private methods - Get redirect, Set common

        /// <summary>
        /// Uses the entrytype parameter to determine which page to redirect to.
        /// </summary>
        /// <param name="entrytype">Entry Type</param>
        /// <returns>Full URL for redirection</returns>
        private string GetRedirectUrl(string entrytype)
        {
            string pageURL = String.Empty;
            //create a page controller object in order to get the page transfer details for the 
            //required mode
            IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];
            PageTransferDetails pageTransferDetails;

            // Check if an entry type is provided, that its correct. 
            // Not providing one is ok, as we can assume requester wanted the findnearest as they used this landingpage url
            if ((entrytype == string.Empty) || (entrytype == "si"))
            {
                switch (entrytype)
                {
                    case "si":
                        pageTransferDetails = pageController.GetPageTransferDetails(PageId.StopInformation);
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

                string message = "StopInformationLandingPage request was made with an incorrect parameter entrytype: " + entrytype
                    + " . Expected entry type 'si' ";
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

        
        #region Private methods - query string parameters
        /// <summary>
        /// Return parameters from querystring arguments.
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns>String lookup of the parameter</returns>
        private string GetValidSingleParam(string paramName)
        {
            string tempCache = string.Empty;

            switch (paramName)
            {
                //Protect against XSS attacks (cross site scripting) by HTML Encoding the text.
                // Some of these parameter strings are displayed as is on portal, so must be made safe.				
                case "id":
                case "et":
                case "sd":
                case "st":
                case "ef":
                    tempCache = HttpUtility.UrlDecode(Page.Request.Params.Get(paramName));
                    break;
                case "enc":
                    tempCache = Page.Request.Params.Get(paramName);
                    break;
                default:
                    tempCache = string.Empty;
                    break;
            }

            if (tempCache != null)
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
                ITDCrypt decryptionEngine = (ITDCrypt)TDServiceDiscovery.Current[ServiceDiscoveryKey.Crypto];
                string decryptedText = decryptionEngine.AsymmetricDecrypt(encrypteddata);
                resultsCache = decryptedText.Split(SECTION_SEP);
            }
            catch (System.ArgumentNullException)
            {
                //enc argument has not been found
                resultsCache = new string[0];
            }

            return resultsCache;
        }

        private void GetEncryptedStopInformationParameters(string[] decodedInput)
        {
            // Ensure the size of the decoded input
            string[] stopType_section = decodedInput[STOPTYPE_SECTION].Split(SUBSECTION_SEP);

            // Verify/fail if place_section length != 2 and that part place_section[ID_KEY] == "st"
            if ((stopType_section.Length != 2) || (stopType_section[STOPTYPE_KEY].CompareTo("st") != 0))
            {
                string message = "StopInformationLandingPage encrypted parameter StopDataType provided does not conform to the sd=[StopDataType] format.";

                LogError(message, partnerId);

                //Redirect to the default page set in GetRedirectUrl()
                Response.Redirect(complete_url);
            }
            else
            {
                stopDataType = stopType_section[STOPTYPE_VAL];
            }

            string[] stopData_section = decodedInput[STOPDATA_SECTION].Split(SUBSECTION_SEP);

            if ((stopData_section.Length != 2) || (stopData_section[STOPDATA_KEY].CompareTo("sd") != 0))
            {
                string message = "StopInformationLandingPage encrypted parameter StopData provided does not conform to the sd=[StopData] format.";

                LogError(message, partnerId);

                //Redirect to the default page set in GetRedirectUrl()
                Response.Redirect(complete_url);
            }
            else
            {
                stopData = stopData_section[STOPDATA_VAL];
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

            if (Page.Request.RequestType == HTTP_REQUEST_TYPE_GET)
            {
                string urlText = HttpUtility.UrlDecode(Page.Request.RawUrl);

                int startIndex = urlText.IndexOf(HTTP_REQUEST_ID_STRING1);

                if (startIndex > 0)
                {
                    startIndex += HTTP_REQUEST_ID_STRING1.Length;
                }
                else
                {
                    startIndex = urlText.IndexOf(HTTP_REQUEST_ID_STRING2);

                    if (startIndex > 0)
                    {
                        startIndex += HTTP_REQUEST_ID_STRING2.Length;
                    }
                }

                if (startIndex > 0)
                {
                    int endIndex = urlText.IndexOf(AMPERSAND, startIndex);

                    if (endIndex > 0)
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

            if (safePartnerId.StartsWith(PREFIX_HTTPS))
            {
                safePartnerId = safePartnerId.Substring(PREFIX_HTTPS.Length);
            }
            else if (safePartnerId.StartsWith(PREFIX_HTTP))
            {
                safePartnerId = safePartnerId.Substring(PREFIX_HTTP.Length);
            }

            if (safePartnerId.StartsWith(PREFIX_WWW))
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
            string[] vendor_section = decodedInput[ID_SECTION].Split(SUBSECTION_SEP);
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

        #region Private methods - Stop data type

        /// <summary>
        /// Returns TDCodeType from stop data type query string
        /// </summary>
        /// <param name="stopDataType">stop data type query string value</param>
        /// <returns>TDCodeType</returns>
        private TDCodeType GetStopCodeDataType(string stopDataType)
        {
            string stopType = stopDataType.ToLower().Trim();

            if (stopType != "n" & stopType != "c" & stopType != "i" & stopType != "s")
            {
                validParameters = false;
            }

            TDCodeType codeType = TDCodeType.NAPTAN;

            switch (stopType)
            {
                case "n":
                    codeType = TDCodeType.NAPTAN;
                    break;

                case "c":
                    codeType = TDCodeType.CRS;
                    break;

                case "i":
                    codeType = TDCodeType.IATA;
                    break;

                case "s":
                    codeType = TDCodeType.SMS;
                    break;

            }   

            return codeType;
        }

        #endregion

        #region Validate Stop Data
        /// <summary>
        /// Validates the stop data entered
        /// </summary>
        /// <param name="stopData">stop data</param>
        /// <returns>true if stop data is valid</returns>
        private bool StopCodeResolved(string stopData)
        {
            TDCodeDetail[] codedetail = null;
            try
            {
                // get the Code Gazetter reference
                ITDCodeGazetteer cg = (ITDCodeGazetteer)TDServiceDiscovery.Current[ServiceDiscoveryKey.CodeGazetteer];

                codedetail = cg.FindCode(stopData);
                try
                {
                    if (codedetail.Length == 0)
                    {
                        // No data has been found so try to find data from the NaPTAN cache
                        NaptanCacheEntry nce = NaptanLookup.Get(stopData, "");
                        if (nce.Found == true)
                        {
                            codedetail = new TDCodeDetail[1];
                            codedetail[0] = new TDCodeDetail();
                            codedetail[0].Code = nce.Naptan;
                            codedetail[0].CodeType = TDCodeType.NAPTAN;
                            codedetail[0].Description = nce.Description;
                            codedetail[0].Easting = nce.OSGR.Easting;
                            codedetail[0].Locality = nce.Locality;
                            codedetail[0].NaptanId = nce.Naptan;
                            codedetail[0].Northing = nce.OSGR.Northing;
                            switch (nce.Naptan.Substring(0, 4))
                            {
                                case ("9000"):
                                    {
                                        codedetail[0].ModeType = TDModeType.Coach;
                                        break;
                                    }
                                case ("9100"):
                                    {
                                        codedetail[0].ModeType = TDModeType.Rail;
                                        break;
                                    }
                                case ("9200"):
                                    {
                                        codedetail[0].ModeType = TDModeType.Air;
                                        break;
                                    }
                                case ("9300"):
                                    {
                                        codedetail[0].ModeType = TDModeType.Ferry;
                                        break;
                                    }
                                case ("9400"):
                                    {
                                        codedetail[0].ModeType = TDModeType.Metro;
                                        break;
                                    }
                                default:
                                    {
                                        codedetail[0].ModeType = TDModeType.Bus;
                                        break;
                                    }
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    // Log the exception
                    OperationalEvent operationalEvent = new OperationalEvent
                        (TDEventCategory.Business, TDTraceLevel.Error, "Stop Information Landing Page Exception." + exception.Message);

                    Logger.Write(operationalEvent);
                }



            }

            catch (Exception exception)
            {
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.Business, TDTraceLevel.Error, "Stop Information Landing Page Exception." + exception.Message);

                Logger.Write(operationalEvent);
            }

            if (codedetail == null)
            {
                return false;
            }
            return codedetail.Length > 0;
        }
        #endregion

        #region Private method - Reset default page session values

        /// <summary>
        /// Method to reset the session values needed by the default page
        /// </summary>
        private void ResetDefaultPageSessionValues()
        {
            // Going to the default page (FindMapInput), so reset the location details it uses
            InputPageState inputPageState = TDSessionManager.Current.InputPageState;

            inputPageState.MapLocation = new TDLocation();
            inputPageState.MapLocationSearch = new LocationSearch();
            inputPageState.MapLocationControlType = new LocationSelectControlType(ControlType.Default);

            inputPageState.MapLocation.SearchType = SearchType.MainStationAirport;
            inputPageState.MapLocationSearch.SearchType = SearchType.MainStationAirport;
        }

        #endregion

        #endregion

        #region Error logging
        /// <summary>
        /// Write operational events to the log, logging ip address and Partner ID
        /// </summary>
        /// <param name="description">The description of the error</param>
        /// <param name="partnerId">The error id</param>
        private void LogError(string description, string partnerId)
        {
            StringBuilder message = new StringBuilder(string.Empty);
            message.Append(description);
            message.Append(" Partner ID : ");
            message.Append(partnerId);
            message.Append(" client-ip : ");
            message.Append(Page.Request.UserHostAddress.ToString(CultureInfo.InvariantCulture));
            message.Append(" url-referrer : ");
            message.Append(Page.Request.UrlReferrer);

            OperationalEvent oe = new OperationalEvent(
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
        }
        #endregion
    }
}
